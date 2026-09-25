using CRM.Application.Common.DTOs.Lessons.Request;
using FluentValidation;

namespace CRM.Application.Features.Lessons.Commands.CreateLesson;

public class CreateLessonCommandValidator : AbstractValidator<CreateLessonCommand>
{
    public CreateLessonCommandValidator()
    {
        RuleFor(x => x.GroupId).NotEmpty();
        RuleFor(x => x.Request).SetValidator(new CreateLessonRequestValidator());
    }
}

public class CreateLessonRequestValidator : AbstractValidator<CreateLessonRequest>
{
    public CreateLessonRequestValidator()
    {
        RuleFor(x => x.WeekNumber).GreaterThan(0);
        RuleFor(x => x.Title).MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(2000);
        RuleFor(x => x.HomeworkDescription).MaximumLength(2000);
        RuleFor(x => x.MaterialUrl).MaximumLength(500);
    }
}
