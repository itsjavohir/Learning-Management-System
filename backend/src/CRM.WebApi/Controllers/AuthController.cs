using System.Security.Claims;
using CRM.Application.Common.DTOs.Login.Request;
using CRM.Application.Features.Auth.Commands.ChangePassword;
using CRM.Application.Features.Auth.Commands.ForgotPassword;
using CRM.Application.Features.Auth.Commands.RefreshToken;
using CRM.Application.Features.Auth.Commands.ResetPassword;
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
    [HttpPost("refresh-token")]

    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request,CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RefreshTokenCommand(request),cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Data);
    }
    
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ForgotPasswordCommand(request), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new ResetPasswordCommand(request), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }
}

