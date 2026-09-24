using CRM.Domain.Common;

namespace CRM.Domain.Entities;

public class GroupStudent : BaseEntity
{
    public Guid GroupId { get; set; }
    public Group Group { get; set; } = null!;

    public Guid StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LeftAt { get; set; }
    public bool IsActive { get; set; } = true;
    public string? RemoveReason { get; set; }

    public Guid? TransferredFromGroupStudentId { get; set; }
    public GroupStudent? TransferredFrom { get; set; }

    public Guid? TransferredToGroupStudentId { get; set; }
    public GroupStudent? TransferredTo { get; set; }
}
