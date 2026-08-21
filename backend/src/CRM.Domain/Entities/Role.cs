using CRM.Domain.Common;

namespace CRM.Domain.Entities;

public class Role:BaseEntity
{
  public string Name { get; set; } = null!;
  public string? Description { get; set; }
  public ICollection<User> Users { get; set; } = new List<User>();

}
