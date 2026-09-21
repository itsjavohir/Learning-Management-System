using CRM.Application.Common.DTOs.Mentors.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Mentors.Queries.GetMentorById;

public record GetMentorByIdQuery(Guid Id) : IRequest<Result<MentorResponse>>;