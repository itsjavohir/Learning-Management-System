using CRM.Application.Common.DTOs.Login.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Application.Interfaces.Services;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Auth.Commands.ChangePassword;

public class ChangePasswordCommandHandler(
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    ITokenGenerator tokenGenerator)
 : IRequestHandler<ChangePasswordCommand, Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle (ChangePasswordCommand command,CancellationToken cancellationToken)
    {
        var request = command.Request;
         if(request.NewPassword != request.ConfirmPassword)
        {
          return Result<LoginResponse>.Fail("New password does not match confirm password", ErrorType.Validation); 
       }
        var user = await unitOfWork.User.GetByIdAsync(command.UserId,cancellationToken);
        if(user is null)
        {
            return Result<LoginResponse>.Fail("User Not Found!",ErrorType.NotFound);
        }
        var isVerified = passwordHasher.Verify(request.OldPassword,user.PasswordHash);
        if (!isVerified)
        {
            return Result<LoginResponse>.Fail("Old password is not correct", ErrorType.Unauthorized);  
        }
        var passwordHash = passwordHasher.Hash(request.NewPassword);
        var newAccessToken = tokenGenerator.GenerateAccessToken(user);
        var newRefreshToken = tokenGenerator.GenerateRefreshToken();

        user.PasswordHash = passwordHash;
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        user.MustChangePassword = false;
        unitOfWork.User.Update(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new LoginResponse(
             AccessToken : newAccessToken,
             RefreshToken : newRefreshToken,
             MustChangePassword:false


        );

        return Result<LoginResponse>.Ok(response);

    }
}

