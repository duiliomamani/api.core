using AspNetCore.Common.Exceptions;
using AspNetCore.Common.Utils;
using AspNetCore.Common.Wrappers;

namespace Api.Notification.Municipio.Perico.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogInformation("ExceptionMiddleware {StackTrace} {Message}", e.StackTrace, e.Message);

                var response = context.Response;
                response.ContentType = "application/json";

                var responseModel = new Response
                {
                    IsSuccessfull = false,
                    Message = e.Message,
                    Errors = e is ValidationException validateException ? validateException.Errors : default
                };

                response.StatusCode = e switch
                {
                    ApiException _ => StatusCodes.Status400BadRequest,// custom application error
                    UnauthorizedAccessException _ => StatusCodes.Status401Unauthorized,// custom application error
                    ForbiddenAccessException _ => StatusCodes.Status403Forbidden,// custom application error
                    ValidationException _ => StatusCodes.Status400BadRequest,  // custom application error
                    KeyNotFoundException _ => StatusCodes.Status404NotFound,// not found error
                    _ => StatusCodes.Status500InternalServerError,// unhandled error
                };

                var result = JsonHelper.Serialize(responseModel);

                await response.WriteAsync(result);
            }
        }
    }
}
