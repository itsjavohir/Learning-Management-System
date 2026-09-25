using CRM.Application.Common.DTOs.Profiles.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Profiles.Queries.GetProfile;

public record GetProfileQuery(Guid UserId) : IRequest<Result<ProfileResponse>>;
