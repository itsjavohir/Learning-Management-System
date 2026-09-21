using CRM.Application.Common.DTOs.Course.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Courses.Command.UpdateCourse;

public class UpdateCourseCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCourseCommand, Result<CourseResponse>>
{
    public async Task<Result<CourseResponse>> Handle(
        UpdateCourseCommand command,
        CancellationToken cancellationToken)
    {
        var course = await unitOfWork.Course.GetByIdAsync(
            command.Id,
            cancellationToken);

        if (course is null)
        {
            return Result<CourseResponse>.Fail("Course not found",ErrorType.NotFound);
        }

        course.Name = command.Request.Name;
        course.Description = command.Request.Description;
        course.DurationWeeks = command.Request.DurationWeeks;
        course.IsActive = command.Request.IsActive;

        unitOfWork.Course.Update(course);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new CourseResponse(
            course.Id,
            course.Name,
            course.Description,
            course.DurationWeeks,
            course.IsActive
        );

        return Result<CourseResponse>.Ok(response);
    }
}