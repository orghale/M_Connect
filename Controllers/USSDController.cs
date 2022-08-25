using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M_Connect.Controllers
{
    [Authorize]
    [ApiController]
    // [Route("ussd")]
    public class USSDController : ControllerBase
    {

        private readonly ILogger<USSDController> _logger;

        public USSDController(ILogger<USSDController> logger)
        {
            _logger = logger;
        }


        [HttpPost("callback")]
        [ProducesResponseType(typeof(CallbackRes), 200)]
        public async Task<IActionResult> CallbackAction(Callback r)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    _logger.LogInformation($"Invalid request: Code not valid");
                    return StatusCode(400, $"Invalid request: Code not valid");
                }

                var links = new List<Link>();

                var response = new CallbackRes
                {
                    url = $"{ new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port.Value, "ussd")}",
                    timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff"),
                    state = new State
                    {
                        description = "Outgoing session - Message sent",
                        code = 1
                    },
                    split_page = 0,
                    alias = r.alias
                };


                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("ussd")]
        [ProducesResponseType(typeof(Response_Object), 200)]
        public async Task<IActionResult> USSD(string code)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(code))
                {
                    _logger.LogInformation($"Invalid request: Code not valid");
                    return StatusCode(400, $"Invalid request: Code not valid");
                }

               // var links = new List<Link>();

                var response = new Response_Object
                {
                    title = "Your Service:",
                    message = "Ask your question",
                    form = new Form
                    {
                        url = $"{ new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port.Value, "question")}",
                        type = "text",
                        method = "get",
                        input = "user-response-to-question"
                    },
                    links = new List<Link>() {
                        new Link {content = "My account",url= $"{ new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port.Value, "account")}" },
                        new Link {content ="Terms and conditions",url= $"{ new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port.Value, "TandC")}" }
                    },
                    page = new Page
                    {
                        menu = "true",
                        history = "true",
                        navigation_keywords = "true",
                        callback = $"{ new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port.Value, "callback")}",
                        method = "Post"
                    }
                };


                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("account")]
        [ProducesResponseType(typeof(Response_Object), 200)]
        public async Task<IActionResult> MyAccount()
        {
            try
            {
              
                var response = new Response_Object
                {
                    title = "My account:",
                    message = "Press 1 to continue",
                    //form = new Form
                    //{
                    //    url = "https://your_service_host:port/question",
                    //    type = "text",
                    //    method = "get"
                    //},
                    //links = new List<Link>() {
                    //    new Link {content = "My account",url= "https://your_service_host:port/account" },
                    //    new Link {content ="Terms and conditions",url= "https://your_service_host:port/TandC" }
                    //},
                    page = new Page
                    {
                        menu = "true",
                        history = "true",
                        navigation_keywords = "true",
                        callback = $"{ new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port.Value, "callback")}",
                        method = "Post"
                    }
                };


                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("TandC")]
        [ProducesResponseType(typeof(Response_Object), 200)]
        public async Task<IActionResult> TermsandConditions()
        {
            try
            {
               
                var links = new List<Link>();

                var response = new Response_Object
                {
                    title = "Terms and conditions:",
                    message = "Press 1 to continue",
                    //form = new Form
                    //{
                    //    url = "https://your_service_host:port/question",
                    //    type = "text",
                    //    method = "get"
                    //},
                    //links = new List<Link>() {
                    //    new Link {content = "My account",url= "https://your_service_host:port/account" },
                    //    new Link {content ="Terms and conditions",url= "https://your_service_host:port/TandC" }
                    //},
                    page = new Page
                    {
                        menu = "true",
                        history = "true",
                        navigation_keywords = "true",
                        callback = $"{ new UriBuilder(Request.Scheme, Request.Host.Host, Request.Host.Port.Value, "callback")}",
                        method = "Post"
                    }
                };


                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return StatusCode(500, ex.Message);
            }
        }



    }
}
