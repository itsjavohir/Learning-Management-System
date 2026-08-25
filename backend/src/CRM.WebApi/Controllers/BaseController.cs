using CRM.Application.Common.Wrappers;
using CRM.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApi.Controllers;

public class BaseController : ControllerBase
{
    protected IActionResult HandleError<T>(Result<T> result)
    {
        return result.Type switch
        {
            ErrorType.NotFound => NotFound(result.Error),
            ErrorType.Conflict => Conflict(result.Error),
            ErrorType.Validation => BadRequest(result.Error),
            ErrorType.Unauthorized => Unauthorized(result.Error),
            ErrorType.Forbidden => Forbid(),
            _ => StatusCode(500, result.Error)
        };
    }
}