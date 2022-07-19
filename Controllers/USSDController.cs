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
    [Route("[controller]")]
    public class USSDController : ControllerBase
    {

        private readonly ILogger<USSDController> _logger;

        public USSDController(ILogger<USSDController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(typeof(Response_Object), 200)]
        public async Task<IActionResult> Get(string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    _logger.LogInformation($"Invalid request: Code not valid");
                    return StatusCode(400, $"Invalid request: Code not valid");
                }

                var links = new List<Link>();

                var response = new Response_Object
                {
                    title = "Your Service:",
                    message = "Ask your question",
                    form = new Form
                    {
                        url = "https://your_service_host:port/question",
                        type = "text",
                        method = "get"
                    },
                    links = new List<Link>() {
                        new Link {content = "My account",url= "https://your_service_host:port/account" },
                        new Link {content ="Terms and conditions",url= "https://your_service_host:port/TandC" }
                    },
                    page = new Page
                    {
                        menu = "true",
                        history = "true",
                        navigation_keywords = "true"
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
