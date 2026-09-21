using CRM.Application.Common.DTOs.Mentors.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Mentors.Queries;

public record GetMentorProfileQuery(Guid UserId):IRequest<Result<MentorProfileResponse>>;

