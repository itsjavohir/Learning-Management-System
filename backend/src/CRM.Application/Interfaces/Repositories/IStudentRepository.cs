using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface IStudentRepository
{
    Task AddAsync (Student student,CancellationToken cancellationToken);
     Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Student>> GetAllAsync (CancellationToken cancellationToken);
    void Update (Student student);
    void Delete (Student student);
}
