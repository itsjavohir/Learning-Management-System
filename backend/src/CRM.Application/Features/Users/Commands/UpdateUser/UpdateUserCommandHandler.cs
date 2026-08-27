using CRM.Application.Common.DTOs.Users.Request;
using CRM.Application.Common.DTOs.Users.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IUnitOfWork unitOfWork)
:IRequestHandler<UpdateUserCommand,Result<UpdateUserResponse>>
{

    public async Task<Result<UpdateUserResponse>> Handle (UpdateUserCommand command,CancellationToken cancellationToken)
    {
        var request = command.Request;
        var user = await unitOfWork.User.GetByIdAsync(command.Id,cancellationToken);
        if(user is null)
        {
            return Result<UpdateUserResponse>.Fail("User Not Found",ErrorType.NotFound);
        }

        var existingUser = await unitOfWork.User.GetByPhoneNumberAsync(request.PhoneNumber,cancellationToken);
        if(existingUser != null && existingUser.Id != command.Id)
        {
              return Result<UpdateUserResponse>.Fail("Phone number already in use", ErrorType.Conflict);
        }

        var existingUserByEmail = await unitOfWork.User.GetByEmailAsync(request.Email,cancellationToken);
        if(existingUserByEmail != null && existingUserByEmail.Id != command.Id)
        {
            return Result<UpdateUserResponse>.Fail("Email already in use", ErrorType.Conflict);
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        user.Email = request.Email;

        unitOfWork.User.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new UpdateUserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.PhoneNumber,
            user.Email,
            user.Role.Name,
            user.IsActive
        );

        return Result<UpdateUserResponse>.Ok(response);
    }
    
}
