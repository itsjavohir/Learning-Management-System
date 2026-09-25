using CRM.Application.Common.DTOs.GroupStudents.Request;
using CRM.Application.Features.GroupStudents.Commands.AddStudentToGroup;
using CRM.Application.Features.GroupStudents.Commands.RemoveStudentFromGroup;
using CRM.Application.Features.GroupStudents.Commands.TransferStudent;
using CRM.Application.Features.GroupStudents.Queries.GetGroupStudents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApi.Controllers;

[ApiController]
[Route("api/groups/{groupId:guid}/students")]
public class GroupStudentsController(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetGroupStudents(Guid groupId, [FromQuery] bool includeInactive, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetGroupStudentsQuery(groupId, includeInactive), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddStudent(Guid groupId, [FromBody] AddStudentToGroupRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new AddStudentToGroupCommand(groupId, request), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{groupStudentId:guid}/remove")]
    public async Task<IActionResult> RemoveStudent(Guid groupId, Guid groupStudentId, [FromBody] RemoveStudentFromGroupRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new RemoveStudentFromGroupCommand(groupStudentId, request), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{groupStudentId:guid}/transfer")]
    public async Task<IActionResult> TransferStudent(Guid groupId, Guid groupStudentId, [FromBody] TransferStudentRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new TransferStudentCommand(groupStudentId, request), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }
}
