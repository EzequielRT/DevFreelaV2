using DevFreela.Application.Models.View;
using DevFreela.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace DevFreela.API.Middlewares;

public class GlobalExceptionHandlerMiddleware : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";

        if (exception is DomainException domainEx)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var result = new ResultViewModel<object>(null, false, domainEx.Message, HttpStatusCode.BadRequest);
            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken);
            return true;
        }

        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var genericResult = new ResultViewModel<object>(null, false, "Erro interno no servidor", HttpStatusCode.InternalServerError);
        await httpContext.Response.WriteAsJsonAsync(genericResult, cancellationToken);
        return true;
    }
}
