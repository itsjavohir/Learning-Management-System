using CRM.Application.Common.DTOs.Course.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using MediatR;

namespace CRM.Application.Features.Courses.Queries.GetAllCourses;

public class GetAllCoursesQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllCoursesQuery, Result<List<CourseResponse>>>
{
    public async Task<Result<List<CourseResponse>>> Handle(
        GetAllCoursesQuery query,
        CancellationToken cancellationToken)
    {
        var courses = await unitOfWork.Course
            .GetAllAsync(cancellationToken);

        var response = courses
            .Select(course => new CourseResponse(
                course.Id,
                course.Name,
                course.Description,
                course.DurationWeeks,
                course.IsActive
            ))
            .ToList();

        return Result<List<CourseResponse>>.Ok(response);
    }
}

