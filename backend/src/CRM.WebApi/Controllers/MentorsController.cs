using System.Security.Claims;
using CRM.Application.Common.DTOs.Mentors.Request;
using CRM.Application.Features.Mentors.Queries.GetMentorProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CRM.Application.Features.Mentors.Commands;
using CRM.Application.Features.Mentors.Queries;

namespace CRM.WebApi.Controllers;

[ApiController]
[Route("api/mentors")]
public class MentorsController(IMediator mediator) : BaseController
{
    [Authorize(Roles = "Mentor")]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await mediator.Send(new GetMentorProfileQuery(userId), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [Authorize(Roles = "Mentor")]
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateMentorProfileRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await mediator.Send(new UpdateMentorProfileCommand(userId, request), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }
}