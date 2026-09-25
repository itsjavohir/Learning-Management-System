using CRM.Application.Common.DTOs.Lessons.Request;
using FluentValidation;

namespace CRM.Application.Features.Lessons.Commands.UpdateLesson;

public class UpdateLessonCommandValidator : AbstractValidator<UpdateLessonCommand>
{
    public UpdateLessonCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Request).SetValidator(new UpdateLessonRequestValidator());
    }
}

public class UpdateLessonRequestValidator : AbstractValidator<UpdateLessonRequest>
{
    public UpdateLessonRequestValidator()
    {
        RuleFor(x => x.WeekNumber).GreaterThan(0);
        RuleFor(x => x.Title).MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(2000);
        RuleFor(x => x.HomeworkDescription).MaximumLength(2000);
        RuleFor(x => x.MaterialUrl).MaximumLength(500);
    }
}
