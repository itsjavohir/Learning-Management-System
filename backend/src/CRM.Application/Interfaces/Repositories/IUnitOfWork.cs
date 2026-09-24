namespace CRM.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IUserRepository User {get;}
    IStudentRepository Student {get;}
    IMentorRepository Mentor {get;}
    IVerificationCodeRepository VerificationCode {get;}
    IRoleRepository Role { get; }
    ICourseRepository Course {get;}
    IGroupRepository Group {get;}
    IGroupStudentRepository GroupStudent { get; }
    IProfileRepository Profile { get; }
    ILessonRepository Lesson { get; }
    IThemeSettingsRepository ThemeSettings { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
