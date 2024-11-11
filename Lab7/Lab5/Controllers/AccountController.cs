using Microsoft.AspNetCore.Mvc;
using Lab5.Models;

namespace Lab5.Controllers
{
    public class AccountController : Microsoft.AspNetCore.Mvc.Controller
    {

        private readonly Auth0Service _auth0Service;

        public AccountController(Auth0Service auth0Service)
        {
            _auth0Service = auth0Service;
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [Route("/signup")]
        public IActionResult Signup()
        {
            return View();
        }

        Auth0Service auth0Service = new Auth0Service();

        [HttpPost]
        [Route("/account/register")]
        public async Task<IActionResult> Registration(string username, string fullname, string password, string passwordConfirm, string phone, string email)
        {
            try
            {
                if (password != passwordConfirm)
                {
                    ViewBag.Error = "Паролі не збігаються!";
                    return View("signup");
                }
                string clientToken = await auth0Service.GetClientToken();
                await auth0Service.CreateUser(username, fullname, password, phone, email, clientToken);
                ViewBag.Message = "Користувача успішно створено!";
                return View("signup");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("signup");
            }
        }

        [HttpPost]
        [Route("/account/loginAuth")]
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
                Console.WriteLine($"Error: {ex.Message}"); // Логування помилок
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Route("/profile")]
        public IActionResult Profile()
        {

            return View();
        }


        [HttpGet]
        [Route("/account/get-profile")]
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