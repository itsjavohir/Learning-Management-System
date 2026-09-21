using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Groups.Commands.DeleteGroup;

public class DeleteGroupCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteGroupCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteGroupCommand command, CancellationToken cancellationToken)
    {
        var group = await unitOfWork.Group.GetByIdAsync(command.Id, cancellationToken);
        if (group is null)
        {
            return Result<bool>.Fail("Group not found", ErrorType.NotFound);
        }

        unitOfWork.Group.Delete(group);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true);
    }
}