using CRM.Application.Common.DTOs.Profiles.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using MediatR;

namespace CRM.Application.Features.Profiles.Queries.GetProfile;

public class GetProfileQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetProfileQuery, Result<ProfileResponse>>
{
    public async Task<Result<ProfileResponse>> Handle(GetProfileQuery query, CancellationToken cancellationToken)
    {
        var profile = await unitOfWork.Profile.GetByUserIdAsync(query.UserId, cancellationToken);

        if (profile is null)
        {
            var empty = new ProfileResponse(
                Guid.Empty,
                query.UserId,
                null, null, null, null, null, null, null, null, null, null
            );

            return Result<ProfileResponse>.Ok(empty);
        }

        var response = new ProfileResponse(
            profile.Id,
            profile.UserId,
            profile.FirstName,
            profile.LastName,
            profile.AvatarUrl,
            profile.Phone,
            profile.DateOfBirth,
            profile.Address,
            profile.TelegramUsername,
            profile.LinkedInUrl,
            profile.GithubUrl,
            profile.AboutMe
        );

        return Result<ProfileResponse>.Ok(response);
    }
}
