using CRM.Application.Interfaces.Repositories;
using CRM.Infrastructure.Persistence.Data;

namespace CRM.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IUserRepository? _user;
    private IStudentRepository? _student;
    private IMentorRepository? _mentor;
    private IVerificationCodeRepository? _verificationCode;
    private IRoleRepository? _rolerepository;
    private ICourseRepository? _course;
    private IGroupRepository ? _group;

    public IUserRepository User =>
        _user ??= new UserRepository(context);

    public IStudentRepository Student =>
        _student ??= new StudentRepository(context);

    public IMentorRepository Mentor =>
        _mentor ??= new MentorRepository(context);

    public IVerificationCodeRepository VerificationCode =>
        _verificationCode ??= new VerificationCodeRepository(context);

        public IRoleRepository Role =>
        _rolerepository ??= new RoleRepository(context);

        public ICourseRepository Course => _course ??= new CourseRepository(context) ;

        public IGroupRepository Group => _group ??= new GroupRepository(context);

    

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }
}