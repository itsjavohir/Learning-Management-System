using CRM.Application.Common.DTOs.Course.Request;
using CRM.Application.Common.DTOs.Course.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Courses.Command.CreateCourse;

public record CreateCourseCommand(CreateCourseRequest Request):IRequest<Result<CourseResponse>>;
