using CRM.Application.Common.DTOs.Mentors.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Mentors.Queries.GetMentorProfile;

public class GetMentorProfileQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetMentorProfileQuery, Result<MentorProfileResponse>>
{
    public async Task<Result<MentorProfileResponse>> Handle(GetMentorProfileQuery query, CancellationToken cancellationToken)
    {
        var mentor = await unitOfWork.Mentor.GetByUserIdAsync(query.UserId, cancellationToken);

        if (mentor is null)
        {
            return Result<MentorProfileResponse>.Fail("Mentor Not Found",ErrorType.NotFound);

        }
        
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