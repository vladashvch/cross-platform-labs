using Lab6.Controllers;
using Lab6.Data;
using Microsoft.AspNetCore.Mvc;

namespace Lab6.Service
{
    public class TokenVerificationService
    {
        private readonly Auth0Service _auth0Service;

        public TokenVerificationService(Auth0Service auth0Service)
        {
            _auth0Service = auth0Service;
        }

        public async Task<ActionResult> VerifyTokenAndExecute(Func<Task<ActionResult>> action, HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return new UnauthorizedObjectResult(new { message = "Authorization token is required." });
            }

            try
            {
                var user = await _auth0Service.GetUser(token);

                if (user == false)
                {
                    return new UnauthorizedObjectResult(new { message = "Invalid or expired token." });
                }

                return await action();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new ObjectResult(new { message = "An error occurred while processing your request.", details = ex.Message })
                {
                    StatusCode = 500
                };
            }
        }
    }
}
