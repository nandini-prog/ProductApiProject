using System.Net;
using System.Text.Json;

namespace API.Middlewares
{
    /// <summary>
    /// Middleware that catches all unhandled exceptions and returns
    /// a clean, consistent JSON response instead of raw stack traces.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Continue the request pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception (you can later push this to Application Insights, ELK, etc.)
                _logger.LogError(ex, "Unhandled exception occurred: {Message}", ex.Message);

                // Return a generic error response
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    statusCode = context.Response.StatusCode,
                    message = "An unexpected error occurred. Please try again later.",
                    details = ex.Message // 👈 optional, remove in production if sensitive
                };

                var json = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }

    // Extension method to register this middleware easily
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
