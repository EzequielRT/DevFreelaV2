using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevFreela.Application.Models.View;

public class ResultViewModel
{
    public ResultViewModel(bool isSuccess = true, string message = "", HttpStatusCode? statusCode = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        StatusCode = statusCode ?? (isSuccess ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
    }

    public bool IsSuccess { get; private set; }
    public string Message { get; private set; }
    public HttpStatusCode StatusCode { get; private set; }

    public static ResultViewModel Success(string message = "", HttpStatusCode statusCode = HttpStatusCode.OK)
        => new ResultViewModel(true, message, statusCode);

    public static ResultViewModel Error(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new ResultViewModel(false, message, statusCode);

    public static ResultViewModel NotFound(string message = "Recurso não encontrado")
        => new ResultViewModel(false, message, HttpStatusCode.NotFound);

    public static ResultViewModel InternalError(string message = "Erro interno no servidor")
        => new ResultViewModel(false, message, HttpStatusCode.InternalServerError);
}

public class ResultViewModel<T> : ResultViewModel
{
    public ResultViewModel(T? data, bool isSuccess = true, string message = "", HttpStatusCode? statusCode = null)
        : base(isSuccess, message, statusCode)
    {
        Data = data;
    }

    public T? Data { get; private set; }

    public static ResultViewModel<T> Success(T data, string message = "", HttpStatusCode statusCode = HttpStatusCode.OK)
        => new ResultViewModel<T>(data, true, message, statusCode);

    public static ResultViewModel<T> Error(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new ResultViewModel<T>(default, false, message, statusCode);

    public static ResultViewModel<T> NotFound(string message = "Recurso não encontrado")
        => new ResultViewModel<T>(default, false, message, HttpStatusCode.NotFound);

    public static ResultViewModel<T> InternalError(string message = "Erro interno no servidor")
        => new ResultViewModel<T>(default, false, message, HttpStatusCode.InternalServerError);
}

public static class ResultViewModelExtensions
{
    public static IActionResult ToActionResult(this ResultViewModel result)
    {
        return new ObjectResult(result)
        {
            StatusCode = (int)result.StatusCode
        };
    }

    public static IActionResult ToActionResult<T>(this ResultViewModel<T> result)
    {
        return new ObjectResult(result)
        {
            StatusCode = (int)result.StatusCode
        };
    }
}