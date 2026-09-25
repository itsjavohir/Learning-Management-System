using CRM.Application.Common.DTOs.Groups.Request;
using FluentValidation;

namespace CRM.Application.Features.Groups.Commands.UpdateGroup;

public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
{
    public UpdateGroupCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Request).SetValidator(new UpdateGroupRequestValidator());
    }
}

public class UpdateGroupRequestValidator : AbstractValidator<UpdateGroupRequest>
{
    public UpdateGroupRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Group name is required")
            .MaximumLength(150);

        RuleFor(x => x.MaxStudents)
            .GreaterThan(0).WithMessage("MaxStudents must be greater than 0")
            .LessThanOrEqualTo(100);
    }
}
