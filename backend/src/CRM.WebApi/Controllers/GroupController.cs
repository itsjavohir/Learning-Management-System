using CRM.Application.Common.DTOs.Groups.Request;
using CRM.Application.Features.Groups.Commands.CreateGroup;
using CRM.Application.Features.Groups.Commands.DeleteGroup;
using CRM.Application.Features.Groups.Commands.UpdateGroup;
using CRM.Application.Features.Groups.Queries.GetAllGroups;
using CRM.Application.Features.Groups.Queries.GetGroupById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApi.Controllers;

[ApiController]
[Route("api/groups")]
public class GroupsController(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllGroupsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetGroupByIdQuery(id), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGroupRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateGroupCommand(request), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateGroupRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateGroupCommand(id, request), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteGroupCommand(id), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return NoContent();
    }
}