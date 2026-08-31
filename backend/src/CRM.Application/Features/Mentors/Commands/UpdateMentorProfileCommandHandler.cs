using CRM.Application.Common.DTOs.Mentors.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Mentors.Commands;

public class UpdateMentorProfileCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateMentorProfileCommand, Result<MentorProfileResponse>>
{
    public async Task<Result<MentorProfileResponse>> Handle(UpdateMentorProfileCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;
        var mentor = await unitOfWork.Mentor.GetByUserIdAsync(command.UserId, cancellationToken);

        if (mentor is null)
        {
            return Result<MentorProfileResponse>.Fail("Mentor not found", ErrorType.NotFound);
        }

        mentor.Specialization = request.Specialization;
        mentor.Bio = request.Bio;
        mentor.ExperienceYears = request.ExperienceYears;

        unitOfWork.Mentor.Update(mentor);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new MentorProfileResponse(
            Id: mentor.User.Id,
            FirstName: mentor.User.FirstName,
            LastName: mentor.User.LastName,
            PhoneNumber: mentor.User.PhoneNumber,
            Email: mentor.User.Email,
            Specialization: mentor.Specialization,
            Bio: mentor.Bio,
            ExperienceYears: mentor.ExperienceYears
        );

        return Result<MentorProfileResponse>.Ok(response);
    }
}