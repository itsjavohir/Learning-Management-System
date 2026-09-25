using CRM.Application.Common.DTOs.Mentors.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Mentors.Queries.GetAllMentorsQuery;

public class GetAllMentorsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllMentorsQuery, Result<List<MentorResponse>>>
{
    public async Task<Result<List<MentorResponse>>> Handle(GetAllMentorsQuery query, CancellationToken cancellationToken)
    {
        var mentors = await unitOfWork.Mentor.GetAllAsync(cancellationToken);

        if (mentors is null || mentors.Count == 0)
        {
            return Result<List<MentorResponse>>.Fail("Mentors Not Found", ErrorType.NotFound);
        }

        var response = mentors.Select(mentor => new MentorResponse(
            Id: mentor.Id,
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
        )).ToList();

        return Result<List<MentorResponse>>.Ok(response);
    }
}