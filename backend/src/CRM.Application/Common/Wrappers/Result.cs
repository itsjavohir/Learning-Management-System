using CRM.Domain.Enums;

namespace CRM.Application.Common.Wrappers;
public class Result<T>
{
    private Result(T? data)
    {
        IsSuccess = true;
        Data = data;
    }

    private Result(string error, ErrorType errorType)
    {
        IsSuccess = false;
        Error = error;
        Type = errorType;   
    }

    public bool IsSuccess { get; }
    public T? Data { get; set; }
    public string? Error { get; }
    public ErrorType? Type { get; }   

    public static Result<T> Ok(T? data)
    {
        return new Result<T>(data);
    }

    public static Result<T> Fail(string error, ErrorType errorType = ErrorType.NotFound)
    {
        return new Result<T>(error, errorType);
    }
}