using System.Security.Claims;
using CRM.Application.Common.DTOs.Students.Request;
using CRM.Application.Features.Students.Commands.UpdateStudentBalance;
using CRM.Application.Features.Students.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApi.Controllers;

[ApiController]
[Route("api/students")]
public class StudentsController(IMediator mediator) : BaseController
{
    [Authorize(Roles = "Student")]
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await mediator.Send(new GetStudentProfileQuery(userId), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}/balance")]
    public async Task<IActionResult> UpdateBalance(Guid id, [FromBody] UpdateStudentBalanceRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateStudentBalanceCommand(id, request), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }
}