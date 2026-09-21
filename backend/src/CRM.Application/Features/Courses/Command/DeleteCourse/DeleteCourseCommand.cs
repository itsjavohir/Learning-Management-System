using CRM.Application.Common.DTOs.Course.Request;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Courses.Command.DeleteCourse;

public record DeleteCourseCommand(DeleteCourseRequest Request):IRequest<Result<bool>>;

