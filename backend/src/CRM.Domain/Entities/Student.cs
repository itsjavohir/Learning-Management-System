using CRM.Domain.Common;

namespace CRM.Domain.Entities;

public class Student:BaseEntity
{
    public Guid UserId { get; set; }
    public decimal Balance { get; set; } = 0;
    public User User { get; set; } = null!;
}
