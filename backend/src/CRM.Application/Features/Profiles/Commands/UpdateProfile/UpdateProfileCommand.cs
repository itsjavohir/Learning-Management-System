using CRM.Application.Common.DTOs.Profiles.Request;
using CRM.Application.Common.DTOs.Profiles.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Profiles.Commands.UpdateProfile;

public record UpdateProfileCommand(Guid UserId, UpdateProfileRequest Request)
    : IRequest<Result<ProfileResponse>>;
