using CRM.Application.Common.DTOs.Lessons.Request;
using CRM.Application.Common.DTOs.Lessons.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Lessons.Commands.UpdateLesson;

public record UpdateLessonCommand(Guid Id, UpdateLessonRequest Request)
    : IRequest<Result<LessonResponse>>;
