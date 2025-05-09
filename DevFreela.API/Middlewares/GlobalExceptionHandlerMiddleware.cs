using DevFreela.Application.Models.View;
using DevFreela.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace DevFreela.API.Middlewares
{
    public class GlobalExceptionHandlerMiddleware : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {            
            if (exception is OperationCanceledException)
            {
                httpContext.Response.StatusCode = 499;

                await httpContext.Response.WriteAsJsonAsync(new
                {
                    message = "Requisição cancelada"
                });

                return true;
            }


            HttpStatusCode statusCode;
            string message = exception.Message;

            if (exception is DomainException)
                statusCode = HttpStatusCode.BadRequest;
            else
                statusCode = HttpStatusCode.InternalServerError;
                // Logue o erro aqui se desejar

            var result = new ResultViewModel<object>(null, false, message, statusCode);

            httpContext.Response.StatusCode = (int)statusCode;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);

            return true;
        }
    }
}
