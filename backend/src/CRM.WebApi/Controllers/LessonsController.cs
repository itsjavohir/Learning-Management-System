using CRM.Application.Common.DTOs.Lessons.Request;
using CRM.Application.Features.Lessons.Commands.CreateLesson;
using CRM.Application.Features.Lessons.Commands.DeleteLesson;
using CRM.Application.Features.Lessons.Commands.UpdateLesson;
using CRM.Application.Features.Lessons.Queries.GetLessonsByGroup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApi.Controllers;

[ApiController]
[Route("api/groups/{groupId:guid}/lessons")]
public class LessonsController(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetByGroup(Guid groupId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetLessonsByGroupQuery(groupId), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [Authorize(Roles = "Admin,Mentor")]
    [HttpPost]
    public async Task<IActionResult> Create(Guid groupId, [FromBody] CreateLessonRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateLessonCommand(groupId, request), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [Authorize(Roles = "Admin,Mentor")]
    [HttpPut("{lessonId:guid}")]
    public async Task<IActionResult> Update(Guid groupId, Guid lessonId, [FromBody] UpdateLessonRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new UpdateLessonCommand(lessonId, request), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return Ok(result.Data);
    }

    [Authorize(Roles = "Admin,Mentor")]
    [HttpDelete("{lessonId:guid}")]
    public async Task<IActionResult> Delete(Guid groupId, Guid lessonId, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteLessonCommand(lessonId), cancellationToken);

        if (!result.IsSuccess)
            return HandleError(result);

        return NoContent();
    }
}
