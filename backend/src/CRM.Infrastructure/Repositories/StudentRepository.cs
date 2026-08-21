using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories;

public class StudentRepository(AppDbContext dbcontext) : IStudentRepository
{
    public async Task AddAsync(Student student, CancellationToken cancellationToken)
    {
        await dbcontext.Students.AddAsync(student,cancellationToken);
    }

    public void Delete(Student student)
    {
        dbcontext.Students.Remove(student);
    }

    public async Task<List<Student>> GetAllAsync(CancellationToken cancellationToken)
    {
       return await dbcontext.Students.AsNoTracking().ToListAsync(cancellationToken);
    }

    public void Update(Student student)
    {
        dbcontext.Students.Update(student);
        student.MarkAsUpdated();
    }

}
