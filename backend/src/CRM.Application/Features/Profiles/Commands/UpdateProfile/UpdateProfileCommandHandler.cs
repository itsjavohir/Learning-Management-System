using CRM.Application.Common.DTOs.Profiles.Response;
using CRM.Application.Common.Extensions;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Profiles.Commands.UpdateProfile;

public class UpdateProfileCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateProfileCommand, Result<ProfileResponse>>
{
    public async Task<Result<ProfileResponse>> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.User.GetByIdAsync(command.UserId, cancellationToken);
        if (user is null)
        {
            return Result<ProfileResponse>.Fail("User not found", ErrorType.NotFound);
        }

        var request = command.Request;
        var profile = await unitOfWork.Profile.GetByUserIdAsync(command.UserId, cancellationToken);

        if (profile is null)
        {
            profile = new Profile
            {
                UserId = command.UserId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                AvatarUrl = request.AvatarUrl,
                Phone = request.Phone,
                DateOfBirth = request.DateOfBirth?.ToUtc(),
                Address = request.Address,
                TelegramUsername = request.TelegramUsername,
                LinkedInUrl = request.LinkedInUrl,
                GithubUrl = request.GithubUrl,
                AboutMe = request.AboutMe
            };

            await unitOfWork.Profile.AddAsync(profile, cancellationToken);
        }
        else
        {
            profile.FirstName = request.FirstName;
            profile.LastName = request.LastName;
            profile.AvatarUrl = request.AvatarUrl;
            profile.Phone = request.Phone;
            profile.DateOfBirth = request.DateOfBirth?.ToUtc();
            profile.Address = request.Address;
            profile.TelegramUsername = request.TelegramUsername;
            profile.LinkedInUrl = request.LinkedInUrl;
            profile.GithubUrl = request.GithubUrl;
            profile.AboutMe = request.AboutMe;

            unitOfWork.Profile.Update(profile);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

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
