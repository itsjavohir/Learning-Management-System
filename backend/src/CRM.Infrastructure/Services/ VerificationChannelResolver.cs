using CRM.Application.Interfaces.Services;
using CRM.Domain.Enums;

namespace CRM.Infrastructure.Services;

public class VerificationChannelResolver(IEnumerable<IVerificationChannel> channels) : IVerificationChannelResolver
{
    public IVerificationChannel Resolve(VerificationChannel channel)
    {
        var found = channels.FirstOrDefault(c => c.Channel == channel);
        if (found is null)
            throw new InvalidOperationException($"No verification channel registered for {channel}");
        return found;
    }
}