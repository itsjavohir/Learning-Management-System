using CRM.Application.Common.DTOs.Mentors.Request;
using CRM.Application.Common.DTOs.Mentors.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Mentors.Commands;

public record UpdateMentorProfileCommand(Guid UserId,UpdateMentorProfileRequest Request) : IRequest<Result<MentorProfileResponse>>;
