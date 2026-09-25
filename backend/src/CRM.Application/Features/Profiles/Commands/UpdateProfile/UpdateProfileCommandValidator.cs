using CRM.Application.Common.DTOs.Profiles.Request;
using CRM.Application.Common.Validators;
using FluentValidation;

namespace CRM.Application.Features.Profiles.Commands.UpdateProfile;

public class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Request).SetValidator(new UpdateProfileRequestValidator());
    }
}

public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
{
    public UpdateProfileRequestValidator()
    {
        RuleFor(x => x.FirstName).MaximumLength(100);
        RuleFor(x => x.LastName).MaximumLength(100);
        RuleFor(x => x.AvatarUrl).MustBeValidUrl();
        RuleFor(x => x.Phone).MustBeValidPhone();
        RuleFor(x => x.DateOfBirth).MustBeValidDateOfBirth();
        RuleFor(x => x.Address).MaximumLength(300);
        RuleFor(x => x.TelegramUsername).MustBeValidTelegramUsername();
        RuleFor(x => x.LinkedInUrl).MustBeValidUrl();
        RuleFor(x => x.GithubUrl).MustBeValidUrl();
        RuleFor(x => x.AboutMe).MaximumLength(2000);
    }
}
