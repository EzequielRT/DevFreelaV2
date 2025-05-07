using DevFreela.Application.Models.View;
using Microsoft.AspNetCore.Diagnostics;

namespace DevFreela.API.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var result = ResultViewModel<object>.InternalError(exception.Message);

            httpContext.Response.StatusCode = (int)result.StatusCode;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);

            return true;
        }
    }
}
