using Authenticator.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authenticator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticatorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthenticatorController(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        /// <summary>
        /// This method is the exposed api to generate and return a token for the given user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetToken")]
        public async Task<IActionResult> GetToken(string userName)
        {
            try
            {
                return Ok(GenerateJSONWebToken(userName));
            }
            catch (Exception exception)
            {
                ExceptionDetail exceptionData = await PushException(exception);

                return BadRequest("There has been an error while generating your token. Kindly check back later or contact the administrator with the following detail: " + exceptionData.ExceptionGUID);
            }
        }


        /// <summary>
        /// This is a helper method to generate JWT Token for the user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private string GenerateJSONWebToken(string userName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, userName)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], claims, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// This is a helper method to push the exception to a service to log it in the database
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task<ExceptionDetail> PushException(Exception exception)
        {
            HttpClient _client = new HttpClient();
            ExceptionDetail exceptionData = new ExceptionDetail(exception.Message, exception.StackTrace, User.Identity.Name, "Shipment.Query");
            var json = JsonConvert.SerializeObject(exceptionData);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            string postURL = _configuration.GetSection("ExceptionHandler").Value;
            var exceptionHandlerResponse = await _client.PostAsync(postURL + "api/ExceptionHandler/Log", stringContent);

            var responseString = await exceptionHandlerResponse.Content.ReadAsStringAsync();
            return exceptionData;
        }
    }
}
