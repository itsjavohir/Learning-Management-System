using FluentValidation;

namespace CRM.Application.Features.Mentors.Commands.UpdateMentorProfile;

public class UpdateMentorProfileCommandValidator : AbstractValidator<UpdateMentorProfileCommand>
{
    public UpdateMentorProfileCommandValidator()
    {
        RuleFor(x => x.Request.Specialization)
            .MaximumLength(200).WithMessage("Specialization must not exceed 200 characters");

        RuleFor(x => x.Request.Bio)
            .MaximumLength(1000).WithMessage("Bio must not exceed 1000 characters");

        RuleFor(x => x.Request.ExperienceYears)
            .GreaterThanOrEqualTo(0).WithMessage("Experience years cannot be negative")
            .LessThanOrEqualTo(60).WithMessage("Experience years seems unrealistic");
    }
}