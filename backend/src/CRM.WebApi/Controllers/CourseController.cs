using CRM.Application.Common.DTOs.Course.Request;
using CRM.Application.Features.Courses.Command.CreateCourse;
using CRM.Application.Features.Courses.Command.DeleteCourse;
using CRM.Application.Features.Courses.Command.UpdateCourse;
using CRM.Application.Features.Courses.Queries.GetAllCourses;
using CRM.Application.Features.Courses.Queries.GetCourseById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApi.Controllers;

[ApiController]
[Route("api/courses")]
public class CourseController(IMediator mediator) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCourseRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new CreateCourseCommand(request),
            cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Data);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(
        [FromBody] DeleteCourseRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new DeleteCourseCommand(request),
            cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateCourseRequest request,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(
            new UpdateCourseCommand(id, request),
            cancellationToken);

        if (!result.IsSuccess)
        {
            return HandleError(result);
        }

        return Ok(result.Data);
    }
[HttpGet]
public async Task<IActionResult> GetAll(
    CancellationToken cancellationToken)
{
    var result = await mediator.Send(
        new GetAllCoursesQuery(),
        cancellationToken);

    if (!result.IsSuccess)
    {
        return HandleError(result);
    }

    return Ok(result.Data);
}

[HttpGet("{id}")]
public async Task<IActionResult> GetById(
    Guid id,
    CancellationToken cancellationToken)
{
    var result = await mediator.Send(
        new GetCourseByIdQuery(id),
        cancellationToken);

    if (!result.IsSuccess)
    {
        return HandleError(result);
    }

    return Ok(result.Data);
}


}