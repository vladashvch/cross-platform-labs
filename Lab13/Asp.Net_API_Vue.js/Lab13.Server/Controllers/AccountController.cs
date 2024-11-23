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
    [Route("[account]")]
    public class AccountController : ControllerBase
    {

        private readonly Auth0Service _auth0Service;

        public AccountController(Auth0Service auth0Service)
        {
            _auth0Service = auth0Service;
        }

        Auth0Service auth0Service = new Auth0Service();

        [HttpPost("/register")]
        public async Task<IActionResult> Registration(string username, string fullname, string password, string passwordConfirm, string phone, string email)
        {
            try
            {
                if (password != passwordConfirm)
                {
                    return BadRequest(new { message = "Паролі не збігаються!" });
                }
                string clientToken = await auth0Service.GetClientToken();
                await auth0Service.CreateUser(username, fullname, password, phone, email, clientToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Проблема" });
            }
        }

        [HttpPost("/login")]
        public async Task<IActionResult> LogIn([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
                {
                    return BadRequest(new { message = "Введіть логін та пароль!" });
                }

                string userToken = await _auth0Service.GetUserToken(loginRequest.Username, loginRequest.Password);
                return Ok(new { Token = userToken });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("/get-profile")]
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