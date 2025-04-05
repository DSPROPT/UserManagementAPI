using Microsoft.AspNetCore.Mvc;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Validate user credentials here (this is just an example)
            if (request.Username == "admin" && request.Password == "password123")
            {
                // Return a simple JSON with a token.
                // In a real app, you'd generate a JWT or fetch a token from an auth server.
                return Ok(new { token = "mysecrettoken" });
            }

            // If credentials are invalid, return 401 Unauthorized
            return Unauthorized();
        }
    }

    // Simple DTO to capture the login request body
    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
