using System.Security.Claims;
using CRM.Application.Common.DTOs.Profiles.Request;
using CRM.Application.Features.Profiles.Commands.UpdateProfile;
using CRM.Application.Features.Profiles.Queries.GetProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApi.Controllers;

[ApiController]
[Route("api/profile")]
[Authorize]
public class ProfilesController(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await mediator.Send(new GetProfileQuery(userId), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await mediator.Send(new UpdateProfileCommand(userId, request), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }
}
