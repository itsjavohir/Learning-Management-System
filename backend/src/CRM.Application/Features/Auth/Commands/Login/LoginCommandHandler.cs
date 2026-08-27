using CRM.Application.Common.DTOs.Login.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Application.Interfaces.Services;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Login;

public class LoginCommandHandler
(IUnitOfWork unitOfWork , ITokenGenerator tokenGenerator,IPasswordHasher passwordHasher)
:IRequestHandler<LoginCommand,Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle (LoginCommand command,CancellationToken cancellationToken)
    {
        var request = command.Request;

        var user = await unitOfWork.User.GetByPhoneNumberAsync(request.PhoneNumber,cancellationToken);
        if(user is null)
        {
            return Result<LoginResponse>.Fail("Invalid phone number or password",ErrorType.Unauthorized);
        }

        var isVerified = passwordHasher.Verify(request.Password,user.PasswordHash);

        if (!isVerified)
        {
            return Result<LoginResponse>.Fail("Invalid phone number or password",ErrorType.Unauthorized);
        }
       
       var accessToken = tokenGenerator.GenerateAccessToken(user);
       var refreshToken = tokenGenerator.GenerateRefreshToken();

       user.RefreshToken = refreshToken;
       user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
       unitOfWork.User.Update(user);
       await unitOfWork.SaveChangesAsync(cancellationToken);
   
        var loginResponse = new LoginResponse
        (
            AccessToken : accessToken,
            RefreshToken : refreshToken,
            MustChangePassword : user.MustChangePassword
        );

       return Result<LoginResponse>.Ok(loginResponse);
        
    }
}
