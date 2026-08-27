using CRM.Domain.Entities;

namespace CRM.Application.Interfaces.Services;

public interface ITokenGenerator
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
