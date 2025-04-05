using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace UserManagementAPI.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log the incoming request
            _logger.LogInformation("Incoming request: {method} {path}",
                context.Request?.Method,
                context.Request?.Path.Value);

            // Let the next middleware run
            await _next(context);

            // Log the outgoing response
            _logger.LogInformation("Outgoing response: {statusCode}",
                context.Response?.StatusCode);
        }
    }
}
