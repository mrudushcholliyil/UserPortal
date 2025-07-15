using Microsoft.AspNetCore.Diagnostics;
using UserPortal.Api.Models;
using UserPortal.Application.Common.Exceptions;

namespace UserPortal.Api.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Here we implemented Global exception handling by inherits IExceptionHandler.
        /// We restricted being showing sensitive information to the user, instead we show a generic error message.
        /// Custom exceptions like BadRequestException and NotFoundException are handled specifically to return appropriate status codes and messages.
        /// All other exceptions are caught and logged, returning a 500 Internal Server Error status code.
        /// TraceId is used to track the request in logs, which is useful for debugging purposes.
        /// </summary>
        /// <param name="httpContext">incoming httpContext</param>
        /// <param name="exception">exception details</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>true once handled</returns>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var traceId = httpContext.TraceIdentifier;

            _logger.LogError(exception, "Exception occurred. TraceId: {TraceId}", traceId);

            BadRequestException? badRequest = exception as BadRequestException;
            NotFoundException? notFound = exception as NotFoundException;

            var errorResponse = new ErrorResponse
            {

                StatusCode = badRequest != null ? StatusCodes.Status400BadRequest
                           : notFound != null ? StatusCodes.Status404NotFound
                           : StatusCodes.Status500InternalServerError,

                Message = badRequest != null ? badRequest.Message :
                          notFound != null ? notFound.Message 
                          :"An unexpected error occurred.",

                TraceId = httpContext.TraceIdentifier,
                Timestamp = DateTime.UtcNow
            };

            httpContext.Response.StatusCode = errorResponse.StatusCode;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);

            return true;
        }
    }
}
