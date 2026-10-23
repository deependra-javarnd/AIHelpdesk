using Microsoft.AspNetCore.Diagnostics;

namespace AIHelpdesk.API.Exceptions
{
    public class GlobalExceptionHandlerTest2 : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandlerTest2> ilogger;

        public GlobalExceptionHandlerTest2(ILogger<GlobalExceptionHandlerTest2> logger)
        {
            ilogger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            ilogger.LogInformation("Global Exception Handler Triggered with {message}", exception.Message);

            var responseObj = new
            {
                status = StatusCodes.Status500InternalServerError,
                details = "Unghandled Exception...",
                message = exception.Message
            };

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(responseObj, cancellationToken);

            return true;
        }
    }
}
