using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace UserManagementAPI.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationMiddleware> _logger;

        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        
        public async Task InvokeAsync(HttpContext context)
        {
            // If the request path is the login route, skip token validation
            if (context.Request.Path.StartsWithSegments("/api/auth/login", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            // Otherwise, continue token validation as before
            var authorizationHeader = context.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrWhiteSpace(authorizationHeader) && 
                authorizationHeader.StartsWith("Bearer "))
            {
                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                if (IsTokenValid(token))
                {
                    await _next(context);
                    return;
                }
            }

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { error = "Unauthorized." });
        }


        private bool IsTokenValid(string token)
        {
            // Placeholder logic. You might use JWT libraries, 
            // validate against a database, or call an identity provider, etc.
            return token == "mysecrettoken"; // Example hardcoded check
        }
    }
}
