using Microsoft.AspNetCore.Mvc;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ErrorsController : ControllerBase
    {
        // This route matches what we'll configure in Program.cs: "/error"
        [Route("/error")]
        public IActionResult HandleError()
        {
            // Here, you could log the error if desired, or capture details for debugging.
            // The Problem() method returns a standardized RFC 7807 response for HTTP APIs.
            return Problem("An unexpected error occurred.", statusCode: 500);
        }
    }
}
