using CRM.Application.Common.DTOs.Groups.Request;
using CRM.Application.Common.DTOs.Groups.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Groups.Commands.UpdateGroup;

public record UpdateGroupCommand(Guid Id, UpdateGroupRequest Request) : IRequest<Result<GroupResponse>>;