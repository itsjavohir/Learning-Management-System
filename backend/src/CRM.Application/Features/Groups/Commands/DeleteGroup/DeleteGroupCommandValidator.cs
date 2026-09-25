using FluentValidation;

namespace CRM.Application.Features.Groups.Commands.DeleteGroup;

public class DeleteGroupCommandValidator : AbstractValidator<DeleteGroupCommand>
{
    public DeleteGroupCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
