using CRM.Application.Common.DTOs.Users;
using CRM.Application.Common.DTOs.Users.Response;
using CRM.Application.Interfaces.Repositories;
using MediatR;

namespace CRM.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUserByIdQuery,UserResponse?>
{
    
    public async Task<UserResponse?> Handle (GetUserByIdQuery request,CancellationToken cancellationToken)
    {
        var user = await unitOfWork.User.GetByIdAsync(request.Id,cancellationToken);

        if (user is null) 
        return null;

         return new UserResponse(
            user.Id, user.FirstName, user.LastName, user.PhoneNumber, user.Email, user.Role.Name, user.IsActive
        );


    }
}
