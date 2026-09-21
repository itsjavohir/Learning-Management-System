
using CRM.Application.Common.DTOs.Course.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Courses.Queries.GetCourseById;

public record GetCourseByIdQuery(
    Guid Id
) : IRequest<Result<CourseResponse>>;

