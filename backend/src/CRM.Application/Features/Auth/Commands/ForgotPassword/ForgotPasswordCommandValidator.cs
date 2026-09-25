using CRM.Domain.Enums;
using FluentValidation;

namespace CRM.Application.Features.Auth.Commands.ForgotPassword;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Request.PhoneNumber)
            .NotEmpty()
            .Matches(@"^\+?\d{9,15}$")
            .When(x => x.Request.Channel == VerificationChannel.Telegram);

        RuleFor(x => x.Request.Email)
            .NotEmpty()
            .EmailAddress()
            .When(x => x.Request.Channel != VerificationChannel.Telegram);
    }
}