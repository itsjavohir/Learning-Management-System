using CRM.Application.Common.DTOs.Lessons.Request;
using CRM.Application.Common.DTOs.Lessons.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Lessons.Commands.CreateLesson;

public record CreateLessonCommand(Guid GroupId, CreateLessonRequest Request)
    : IRequest<Result<LessonResponse>>;
