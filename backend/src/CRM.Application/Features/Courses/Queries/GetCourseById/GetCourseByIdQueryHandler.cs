using CRM.Application.Common.DTOs.Course.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Courses.Queries.GetCourseById;

public class GetCourseByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetCourseByIdQuery, Result<CourseResponse>>
{
    public async Task<Result<CourseResponse>> Handle(
        GetCourseByIdQuery query,
        CancellationToken cancellationToken)
    {
        var course = await unitOfWork.Course
            .GetByIdAsync(query.Id, cancellationToken);

        if (course is null)
        {
            return Result<CourseResponse>.Fail("Course not found",ErrorType.NotFound);
        }

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

