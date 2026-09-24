using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Repositories;

public interface IGroupStudentRepository
{
    Task<GroupStudent?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<GroupStudent?> GetByGroupAndStudentAsync(Guid groupId, Guid studentId, CancellationToken cancellationToken);
    Task<List<GroupStudent>> GetByGroupIdAsync(Guid groupId, bool includeInactive, CancellationToken cancellationToken);
    Task<List<GroupStudent>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken);
    Task<int> CountActiveByGroupIdAsync(Guid groupId, CancellationToken cancellationToken);
    Task AddAsync(GroupStudent groupStudent, CancellationToken cancellationToken);
    void Update(GroupStudent groupStudent);
    void Delete(GroupStudent groupStudent);
}
