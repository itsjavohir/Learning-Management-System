using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Lessons.Commands.DeleteLesson;

public record DeleteLessonCommand(Guid Id) : IRequest<Result<bool>>;
