using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User?> GetByPhoneNumberAsync(string phone, CancellationToken cancellationToken);
    Task<User?> GetByRefreshTokenAsync(string refreshtoken,CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    void Update(User user);
    void Delete(User user);
}