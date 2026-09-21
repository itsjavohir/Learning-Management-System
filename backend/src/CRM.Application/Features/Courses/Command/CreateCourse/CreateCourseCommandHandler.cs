using CRM.Application.Common.DTOs.Course.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using MediatR;

namespace CRM.Application.Features.Courses.Command.CreateCourse;

public class CreateCourseCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCourseCommand, Result<CourseResponse>>
{
    public async Task<Result<CourseResponse>> Handle(
        CreateCourseCommand command,
        CancellationToken cancellationToken)
    {
          var request = command.Request;
        var course = new Course
        {
            Name = request.Name,
            Description = request.Description,
            DurationWeeks = request.DurationWeeks
        };

        await unitOfWork.Course.AddAsync(course, cancellationToken);

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

