namespace CRM.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IUserRepository User {get;}
    IStudentRepository Student {get;}
    IMentorRepository Mentor {get;}
    IVerificationCodeRepository VerificationCode {get;}
    IRoleRepository Role { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
