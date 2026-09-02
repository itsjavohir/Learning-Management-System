using CRM.Application.Common.DTOs.Login.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Application.Interfaces.Services;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Auth.Commands.ResetPassword;

public class ResetPasswordCommandHandler(
    IUnitOfWork unitOfWork,
    ITokenGenerator tokenGenerator,
    IPasswordHasher passwordHasher
):IRequestHandler<ResetPasswordCommand,Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle (ResetPasswordCommand command,CancellationToken cancellationToken)
    {
        var request = command.Request;
        var user = await unitOfWork.User.GetByPhoneNumberAsync(request.PhoneNumber,cancellationToken);
       if (user is null)
        {
          return Result<LoginResponse>.Fail("Invalid or expired code", ErrorType.Validation);
        }
       var verificationcode = await unitOfWork.VerificationCode.GetActiveCodeAsync(user.Id,VerificationCodeType.PasswordReset,cancellationToken);
       if(verificationcode is null)
        {
            return Result<LoginResponse>.Fail("Invalid or expired code", ErrorType.Validation);
        }
        var isCodeValid = passwordHasher.Verify(request.VerifyCode,verificationcode.CodeHash);
        if (!isCodeValid)
        {
            verificationcode.Attempts++;
            unitOfWork.VerificationCode.Update(verificationcode);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<LoginResponse>.Fail("Invalid or expired code", ErrorType.Validation);
        }
        if (request.NewPassword != request.ConfirmPassword)
        {     
             return Result<LoginResponse>.Fail("New password does not match confirm password", ErrorType.Validation);
        }

        var newPasswordHash = passwordHasher.Hash(request.NewPassword);
        user.PasswordHash = newPasswordHash;
        verificationcode.IsUsed = true;

        var accessToken = tokenGenerator.GenerateAccessToken(user);
        var refreshToken = tokenGenerator.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        unitOfWork.User.Update(user);
        unitOfWork.VerificationCode.Update(verificationcode);
        await unitOfWork.SaveChangesAsync(cancellationToken);

     var response = new LoginResponse(
    AccessToken: accessToken,
    RefreshToken: refreshToken,
    MustChangePassword: false
);

return Result<LoginResponse>.Ok(response);

}
}
