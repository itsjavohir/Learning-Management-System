using CRM.Application.Common.DTOs.Mentors.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Mentors.Queries.GetMentorById;

public class GetMentorByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetMentorByIdQuery, Result<MentorResponse>>
{
    public async Task<Result<MentorResponse>> Handle(GetMentorByIdQuery query, CancellationToken cancellationToken)
    {
        var mentor = await unitOfWork.Mentor.GetByIdAsync(query.Id, cancellationToken);

        if (mentor is null)
        {
            return Result<MentorResponse>.Fail("Mentor Not Found", ErrorType.NotFound);
        }

        var response = new MentorResponse(
            Id: mentor.User.Id,
            FirstName: mentor.User.FirstName,
            LastName: mentor.User.LastName,
            PhoneNumber: mentor.User.PhoneNumber,
            Email: mentor.User.Email,
            Specialization: mentor.Specialization,
            Bio: mentor.Bio,
            ExperienceYears: mentor.ExperienceYears,
            LinkedInUrl: mentor.LinkedInUrl,
            GithubUrl: mentor.GithubUrl,
            HireDate: mentor.HireDate,
            IsActive: mentor.IsActive
        );

        return Result<MentorResponse>.Ok(response);
    }
}