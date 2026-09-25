using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface IScheduleRepository
{
    Task<Schedule?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Schedule>> GetByGroupIdAsync(Guid groupId, CancellationToken cancellationToken);
    Task AddAsync(Schedule schedule, CancellationToken cancellationToken);
    void Update(Schedule schedule);
    void Delete(Schedule schedule);
}
