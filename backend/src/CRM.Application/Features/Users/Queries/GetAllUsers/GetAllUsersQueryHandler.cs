using CRM.Application.Common.DTOs.Users;
using CRM.Application.Interfaces.Repositories;
using MediatR;

namespace CRM.Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.User.GetAllAsync(cancellationToken);

        return users.Select(u => new UserDto(
            u.Id, u.FirstName, u.LastName, u.PhoneNumber, u.Email, u.Role.Name, u.IsActive
        )).ToList();
    }
}
