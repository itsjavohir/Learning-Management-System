using CRM.Application.Common.DTOs.Course.Request;
using FluentValidation;

namespace CRM.Application.Features.Courses.Command.UpdateCourse;

public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Request).SetValidator(new UpdateCourseRequestValidator());
    }
}

public class UpdateCourseRequestValidator : AbstractValidator<UpdateCourseRequest>
{
    public UpdateCourseRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Course name is required")
            .MaximumLength(150);

        RuleFor(x => x.Description)
            .MaximumLength(2000);

        RuleFor(x => x.DurationWeeks)
            .GreaterThan(0).WithMessage("Duration must be greater than 0 weeks")
            .LessThanOrEqualTo(104).WithMessage("Duration is unrealistically long");
    }
}
