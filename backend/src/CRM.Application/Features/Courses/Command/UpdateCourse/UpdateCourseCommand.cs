using CRM.Application.Common.DTOs.Course.Request;
using CRM.Application.Common.DTOs.Course.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Courses.Command.UpdateCourse;

public record UpdateCourseCommand(
    Guid Id,
    UpdateCourseRequest Request
) : IRequest<Result<CourseResponse>>;

