using CRM.Application.Common.DTOs.Users;
using CRM.Application.Interfaces.Repositories;
using MediatR;

namespace CRM.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserByIdQuery,UserDto?>
{
    
    public async Task<UserDto?> Handle (GetUserByIdQuery request,CancellationToken cancellationToken)
    {
        var user = await unitOfWork.User.GetUserByIdAsync(request.Id,cancellationToken);

        if (user is null) 
        return null;

         return new UserDto(
            user.Id, user.FirstName, user.LastName, user.PhoneNumber, user.Email, user.Role.Name, user.IsActive
        );


    }
}
