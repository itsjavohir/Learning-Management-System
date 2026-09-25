using CRM.Application.Common.DTOs.Mentors.Request;
using CRM.Application.Common.Validators;
using FluentValidation;

namespace CRM.Application.Features.Mentors.Commands;

public class UpdateMentorProfileCommandValidator : AbstractValidator<UpdateMentorProfileCommand>
{
    public UpdateMentorProfileCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Request).SetValidator(new UpdateMentorProfileRequestValidator());
    }
}

public class UpdateMentorProfileRequestValidator : AbstractValidator<UpdateMentorProfileRequest>
{
    public UpdateMentorProfileRequestValidator()
    {
        RuleFor(x => x.Specialization).MaximumLength(200);
        RuleFor(x => x.Bio).MaximumLength(2000);
        RuleFor(x => x.ExperienceYears).GreaterThanOrEqualTo(0).LessThanOrEqualTo(60);
        RuleFor(x => x.Phone).MustBeValidPhone();
        RuleFor(x => x.LinkedInUrl).MustBeValidUrl();
        RuleFor(x => x.GithubUrl).MustBeValidUrl();
    }
}
