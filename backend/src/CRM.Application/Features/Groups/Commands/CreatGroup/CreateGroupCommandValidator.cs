using CRM.Application.Common.DTOs.Groups.Request;
using FluentValidation;

namespace CRM.Application.Features.Groups.Commands.CreateGroup;

public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
{
    public CreateGroupCommandValidator()
    {
        RuleFor(x => x.Request).SetValidator(new CreateGroupRequestValidator());
    }
}

public class CreateGroupRequestValidator : AbstractValidator<CreateGroupRequest>
{
    public CreateGroupRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Group name is required")
            .MaximumLength(150);

        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("CourseId is required");

        RuleFor(x => x.MentorId)
            .NotEmpty().WithMessage("MentorId is required");

        RuleFor(x => x.MaxStudents)
            .GreaterThan(0).WithMessage("MaxStudents must be greater than 0")
            .LessThanOrEqualTo(100);
    }
}
