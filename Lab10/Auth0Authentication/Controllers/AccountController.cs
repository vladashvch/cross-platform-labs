using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lab5.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Lab5.Controllers
{

    [ApiController]
    [Route("auth")]
    public class AccountController : Controller
    {

        private readonly Auth0Service _auth0Service;

        public AccountController(Auth0Service auth0Service)
        {
            _auth0Service = auth0Service;
        }

        Auth0Service auth0Service = new Auth0Service();

        [HttpPost("/login")]
        public async Task<IActionResult> LogIn([FromBody] LoginRequest loginRequest)
        {
            try
            {
                string userToken = await _auth0Service.GetUserToken(loginRequest.Username, loginRequest.Password);
                return Ok(new { Token = userToken });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("/get-profile")]
        public async Task<IActionResult> GetProfile()
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest(new { message = "Token is required." });
                }

                var userInfo = await _auth0Service.GetUser(token);
                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user profile: {ex.Message}");
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}