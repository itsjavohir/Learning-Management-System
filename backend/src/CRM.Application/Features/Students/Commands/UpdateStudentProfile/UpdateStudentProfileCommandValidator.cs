using CRM.Application.Common.DTOs.Students.Request;
using FluentValidation;

namespace CRM.Application.Features.Students.Commands.UpdateStudentProfile;

public class UpdateStudentProfileCommandValidator : AbstractValidator<UpdateStudentProfileCommand>
{
    public UpdateStudentProfileCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Request).SetValidator(new UpdateStudentProfileRequestValidator());
    }
}

public class UpdateStudentProfileRequestValidator : AbstractValidator<UpdateStudentProfileRequest>
{
    public UpdateStudentProfileRequestValidator()
    {
        RuleFor(x => x.PhotoUrl)
            .Must(BeAValidUrl)
            .When(x => !string.IsNullOrWhiteSpace(x.PhotoUrl))
            .WithMessage("PhotoUrl must be a valid absolute URL.");

        RuleFor(x => x.GithubUrl)
            .Must(BeAValidUrl)
            .When(x => !string.IsNullOrWhiteSpace(x.GithubUrl))
            .WithMessage("GithubUrl must be a valid absolute URL.");

        RuleFor(x => x.TelegramUsername)
            .MaximumLength(32)
            .When(x => !string.IsNullOrWhiteSpace(x.TelegramUsername));

        RuleFor(x => x.AboutMe)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrWhiteSpace(x.AboutMe));

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateTime.UtcNow)
            .When(x => x.DateOfBirth.HasValue)
            .WithMessage("DateOfBirth must be in the past.");
    }

    private static bool BeAValidUrl(string? url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}
