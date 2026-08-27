using System.Security.Claims;
using CRM.Application.Common.DTOs.Login.Request;
using CRM.Application.Features.Auth.Commands.ChangePassword;
using CRM.Application.Features.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApi.Controllers;
[ApiController]
[Route("api/auth")]
public class AuthController(IMediator mediator):BaseController
{
    [HttpPost("login")]

    public async Task<IActionResult> Login ([FromBody]LoginRequest request,CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new LoginCommand(request),cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Data);
    }
    [Authorize]
    [HttpPost("change-password")]

    public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await mediator.Send(new ChangePasswordCommand(userId,request),cancellationToken);

        if (!result.IsSuccess)
        return HandleError(result);

          return Ok(result.Data);
    }
}
