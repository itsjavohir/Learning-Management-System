using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace CRM.Infrastructure.Repositories;

public class CourseRepository(AppDbContext dbcontext) : ICourseRepository
{
    public async Task AddAsync(Course course, CancellationToken cancellationToken)
    {
        await dbcontext.Courses.AddAsync(course, cancellationToken);
    }

    public void Delete(Course course)
    {
        dbcontext.Courses.Remove(course);
    }

    public async Task<List<Course>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbcontext.Courses
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbcontext.Courses
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public void Update(Course course)
    {
        dbcontext.Courses.Update(course);
        course.MarkAsUpdated();
    }
}