using System.Security.Claims;
using CRM.Application.Features.Theme.Commands.SetSeasonOverrideCommand;
using CRM.Application.Features.Theme.Queries.GetCurrentSeasonQuery;
using CRM.Application.Features.Theme.Queries.GetSeasonHistory;
using CRM.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApi.Controllers;

[ApiController]
[Route("api/theme")]
public class ThemeController(IMediator mediator) : BaseController
{
    [AllowAnonymous]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
    [HttpGet("season")]
    public async Task<IActionResult> GetCurrentSeason(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetCurrentSeasonQuery(), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("season/history")]
    public async Task<IActionResult> GetSeasonHistory(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetSeasonHistoryQuery(), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    public record SetSeasonOverrideRequest(Season? Season);

    [Authorize(Roles = "Admin")]
    [HttpPut("season/override")]
    public async Task<IActionResult> SetSeasonOverride(
        [FromBody] SetSeasonOverrideRequest request,
        CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var command = new SetSeasonOverrideCommand(request.Season, userId);
        var result = await mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }
}