using CRM.Application.Common.DTOs.Course.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Courses.Queries.GetAllCourses;

public record GetAllCoursesQuery
    : IRequest<Result<List<CourseResponse>>>;

