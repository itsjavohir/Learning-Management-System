using CRM.Application.Common.DTOs.Login.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Application.Interfaces.Services;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler(
    IUnitOfWork unitOfWork,
    ITokenGenerator tokenGenerator
    ) :IRequestHandler<RefreshTokenCommand,Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle(RefreshTokenCommand command,CancellationToken cancellationToken)
    {
        var request = command.Request;

        var user = await unitOfWork.User.GetByRefreshTokenAsync(request.RefreshToken,cancellationToken);

        if(user is null)
        {
            return Result<LoginResponse>.Fail("Token is not valid !",ErrorType.Unauthorized);
        }
        if( user.RefreshTokenExpiry < DateTime.UtcNow)
        {
            return Result<LoginResponse>.Fail("Refresh token has expired",ErrorType.Unauthorized);
        }
         var accessToken = tokenGenerator.GenerateAccessToken(user);
         var refreshToken = tokenGenerator.GenerateRefreshToken();
        
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        unitOfWork.User.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new LoginResponse (
            AccessToken : accessToken,
            RefreshToken : refreshToken,
            MustChangePassword : user.MustChangePassword
        );

        return Result<LoginResponse>.Ok(response);

    }
}
