using CRM.Application.Common.DTOs.Groups.Response;
using MediatR;

namespace CRM.Application.Features.Groups.Queries.GetAllGroups;

public record GetAllGroupsQuery : IRequest<List<GroupResponse>>;