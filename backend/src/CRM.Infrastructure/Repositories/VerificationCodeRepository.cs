using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using CRM.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories;

public class VerificationCodeRepository(AppDbContext dbcontext) : IVerificationCodeRepository
{
    public async Task AddAsync(VerificationCode code, CancellationToken cancellationToken)
    {
        await dbcontext.VerificationCodes.AddAsync(code,cancellationToken);
    }

    public async Task<VerificationCode?> GetActiveCodeAsync(Guid userId, VerificationCodeType type, CancellationToken cancellationToken)
    {
       return await dbcontext.VerificationCodes.Where
       (
         c => c.UserId == userId
         && c.Type == type
         && !c.IsUsed
         && c.Expiration > DateTime.UtcNow
        )
        .OrderByDescending(c => c.CreatedAt)
        .FirstOrDefaultAsync(cancellationToken);
        
    }


    public void Update(VerificationCode code)
    {
        dbcontext.VerificationCodes.Update(code);
        code.MarkAsUpdated();
    }

}
