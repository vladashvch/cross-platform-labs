using System.IdentityModel.Tokens.Jwt;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        // Check for the Authorization header
        if (context.Request.Headers.ContainsKey("Authorization"))
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Validate the token here (you can use JwtSecurityTokenHandler to validate JWT tokens)
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Invalid token.");
                return;
            }

            // Optionally, you could also verify the token claims here
        }
        else
        {
            // If there is no Authorization header, you might want to protect certain routes
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Token is required.");
            return;
        }

        // Call the next middleware in the pipeline
        await _next(context);
    }
}
