using CRM.Application.Common.DTOs.Users;
using CRM.Application.Common.DTOs.Users.Response;
using CRM.Application.Interfaces.Repositories;
using MediatR;

namespace CRM.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllUsersQuery,List<UserResponse>>
{
    public async Task<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.User.GetAllAsync(cancellationToken);

        return users.Select(u => new UserResponse(
            u.Id, u.FirstName, u.LastName, u.PhoneNumber, u.Email, u.Role.Name, u.IsActive
        )).ToList();
    }
}
