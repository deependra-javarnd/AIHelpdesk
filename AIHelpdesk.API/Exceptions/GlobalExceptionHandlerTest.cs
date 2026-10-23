using AIHelpdesk.API.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace AIHelpdesk.API.Exceptions
{
    public class GlobalExceptionHandlerTest : IExceptionHandler
    {

        private readonly ILogger<GlobalExceptionHandlerTest> _logger;

        public GlobalExceptionHandlerTest(ILogger<GlobalExceptionHandlerTest> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

            _logger.LogInformation("Ticket created successfully with Id {TicketId}", 1111);

            var exceptionResponseObject = new
            {
                status = 500,
                message = "Unhandled Exception Occur.",
                details = exception.Message.ToString()
            };

            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(exceptionResponseObject, cancellationToken);

            return true;
        }

    }
}
