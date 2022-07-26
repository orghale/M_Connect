﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using M_Connect.Encryption;
using Microsoft.Net.Http.Headers;

namespace M_Connect.Controllers
{

    [AttributeUsage(validOn: AttributeTargets.Method)]
    public class Authorize : Attribute, IAsyncActionFilter
    {
        private const string sid = "sid";
        private const string auth = "auth";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var sidHeader = context.HttpContext.Request.Headers.TryGetValue(sid, out var extractedsid);
            var authHeader = context.HttpContext.Request.Headers.TryGetValue(auth, out var extractedauth);

            if (!sidHeader && !authHeader)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Unauthorized Access: Request missing header authorization"
                };
                return;
            }

            if (!sidHeader)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Unauthorized Access: SID was not provided"
                };
                return;
            }

            if (!authHeader)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Unauthorized Access: AUTH was not provided"
                };
                return;
            }


            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var sidKey = appSettings.GetSection("ApiKeyConfig").GetValue<string>("sid");
            var authKey = appSettings.GetSection("ApiKeyConfig").GetValue<string>("auth");

            //validate request sid value against system sid key 
            if (!sidKey.Equals(extractedsid))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Unauthorized Access: SID is not valid"
                };
                return;
            }

            //validate request auth value against system auth key 
            if (!authKey.Equals(extractedauth/*.ToString().DecryptString()*/))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Unauthorized Access: AUTH is not valid"
                };
                return;
            }

            //add request sid value to response header 
            context.HttpContext.Response.Headers.Add(sid, sidKey.ToString());

            //encrypt request auth value using RSA_PKCS1 padding and add to response header 
            context.HttpContext.Response.Headers.Add(auth, authKey.ToString().EncryptString());


            context.HttpContext.Response.Headers[HeaderNames.CacheControl] = "no-cache, private";
            context.HttpContext.Response.Headers[HeaderNames.KeepAlive] = "timeout=5, max=100";
            context.HttpContext.Response.Headers[HeaderNames.Connection] = "Keep-Alive";

            await next();
        }
    }

}

