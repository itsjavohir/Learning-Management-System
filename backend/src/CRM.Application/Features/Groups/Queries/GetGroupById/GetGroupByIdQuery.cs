using CRM.Application.Common.DTOs.Groups.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Groups.Queries.GetGroupById;

public record GetGroupByIdQuery(Guid Id) : IRequest<Result<GroupResponse>>;