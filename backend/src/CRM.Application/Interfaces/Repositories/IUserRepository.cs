using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user,CancellationToken cancellationToken);
    Task<List<User>>GetAllAsync(CancellationToken cancellationToken);
     Task<User?>GetUserByIdAsync(Guid id,CancellationToken cancellationToken);
     void Update (User user);
     void Delete(User user);
}
