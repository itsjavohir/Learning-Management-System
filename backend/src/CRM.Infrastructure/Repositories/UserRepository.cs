using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories;

public class UserRepository(AppDbContext dbcontext) : IUserRepository
{
    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await dbcontext.Users.AddAsync(user, cancellationToken);
    }

    public void Delete(User user)
    {
        dbcontext.Users.Remove(user);
    }

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbcontext.Users
            .AsNoTracking()
            .Include(u => u.Role)
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await dbcontext.Users
            .AsNoTracking()
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbcontext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<User?> GetByPhoneNumberAsync(string phone, CancellationToken cancellationToken)
    {
        return await dbcontext.Users
            .AsNoTracking()
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.PhoneNumber == phone, cancellationToken);
    }

    public void Update(User user)
    {
        dbcontext.Users.Update(user);
        user.MarkAsUpdated();
    }
}