using LoyaltyConsole.API.ApiResponses;
using LoyaltyConsole.Business.Exceptions;
using InvalidDataException = LoyaltyConsole.Business.Exceptions.InvalidDataException;

namespace LoyaltyConsole.API.ExceptionHandler
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            int status = ex switch
            {
                InvalidDataException => StatusCodes.Status400BadRequest,
                ValidationException => StatusCodes.Status400BadRequest,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new ApiResponse<string>
            {
                StatusCode = status,
                ErrorMessage = ex.Message,
                Data = null
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = status;

            return context.Response.WriteAsJsonAsync(response);
        }
    }

}
