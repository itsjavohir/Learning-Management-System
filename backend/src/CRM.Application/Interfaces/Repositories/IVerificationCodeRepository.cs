using CRM.Domain.Entities;
using CRM.Domain.Enums;

namespace CRM.Application.Interfaces.Repositories;

public interface IVerificationCodeRepository
{
    Task<VerificationCode?> GetActiveCodeAsync
    (
        Guid userId ,VerificationCodeType type,CancellationToken cancellationToken
    );
    
    Task AddAsync(VerificationCode code,CancellationToken cancellationToken);
    void Update(VerificationCode code);

}
