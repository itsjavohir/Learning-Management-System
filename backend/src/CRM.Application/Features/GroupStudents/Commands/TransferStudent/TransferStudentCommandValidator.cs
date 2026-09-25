using FluentValidation;

namespace CRM.Application.Features.GroupStudents.Commands.TransferStudent;

public class TransferStudentCommandValidator : AbstractValidator<TransferStudentCommand>
{
    public TransferStudentCommandValidator()
    {
        RuleFor(x => x.GroupStudentId).NotEmpty();
        RuleFor(x => x.Request.ToGroupId).NotEmpty();
    }
}
