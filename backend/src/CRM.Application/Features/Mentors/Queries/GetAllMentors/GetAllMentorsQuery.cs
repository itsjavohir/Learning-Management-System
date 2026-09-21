using CRM.Application.Common.DTOs.Mentors.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Mentors.Queries.GetAllMentorsQuery;

public class GetAllMentorsQuery : IRequest<Result<List<MentorResponse>>>
{
}