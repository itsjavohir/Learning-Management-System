using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand,Result<bool>>
{
    
    public async Task<Result<bool>> Handle (DeleteUserCommand command,CancellationToken cancellationToken)
    {
        var user = await unitOfWork.User.GetByIdAsync(command.Id,cancellationToken);
        if(user is null)
        {
            return Result<bool>.Fail("User not found", ErrorType.NotFound);
        }

        unitOfWork.User.Delete(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<bool>.Ok(true);
    }
}
