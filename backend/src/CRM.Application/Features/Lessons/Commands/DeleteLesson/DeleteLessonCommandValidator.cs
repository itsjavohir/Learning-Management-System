using FluentValidation;

namespace CRM.Application.Features.Lessons.Commands.DeleteLesson;

public class DeleteLessonCommandValidator : AbstractValidator<DeleteLessonCommand>
{
    public DeleteLessonCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
