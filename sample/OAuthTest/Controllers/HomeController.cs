using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OAuthTest.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    [Authorize(Roles = "anonymous")]
    public class HomeController : ControllerBase
    {

        public HomeController(UserContext usedContext)
        {
            
        }

        [HttpGet]
        [Route("Index")]
        public string Index()
        {
            return "I am running";
        }

        
        [HttpGet]
        [Route("SecureContent")]
        public string SecureContent()
        {
            return "Here is secure data";
        }
    }
}