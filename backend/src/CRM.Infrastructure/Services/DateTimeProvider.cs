
using CRM.Application.Interfaces.Services;

namespace CRM.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}