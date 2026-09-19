using CRM.Domain.Enums;

namespace CRM.Application.Interfaces.Services;

public interface IVerificationChannelResolver
{
    IVerificationChannel Resolve(VerificationChannel channel);
}