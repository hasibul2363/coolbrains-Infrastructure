using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace OAuthTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [Route("AnonymousToken")]
        [HttpGet]
        public IActionResult AnonymousToken()
        {
            var token = _authenticationService.GetAnonymousToken(this.HttpContext);




            //try
            //{
            //    var key = Encoding.ASCII.GetBytes
            //        ("2363-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
            //    var claimsPrincipal = new JwtSecurityTokenHandler()
            //        .ValidateToken(token.Token, new TokenValidationParameters
            //        {
            //            ValidAudience = "security.coolbrains.co",
            //            ValidateIssuer = false,
            //            ValidateAudience = false,
            //            ValidateLifetime = false,
            //            ValidateIssuerSigningKey = false,
            //            ValidIssuer = "security.coolbrains.co",
            //            IssuerSigningKey = new SymmetricSecurityKey(key)


            //        }, out var rawValidatedToken);



            //}
            //catch(Exception ex)
            //{

            //}








            return Ok(token);
        }

        //public IActionResult Token()
        //{
        //}
    }
}