using FluentValidation;

namespace CRM.Application.Features.GroupStudents.Commands.RemoveStudentFromGroup;

public class RemoveStudentFromGroupCommandValidator : AbstractValidator<RemoveStudentFromGroupCommand>
{
    public RemoveStudentFromGroupCommandValidator()
    {
        RuleFor(x => x.GroupStudentId).NotEmpty();
        RuleFor(x => x.Request.Reason).MaximumLength(500);
    }
}
