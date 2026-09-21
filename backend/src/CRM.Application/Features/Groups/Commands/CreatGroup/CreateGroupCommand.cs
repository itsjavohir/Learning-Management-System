using CRM.Application.Common.DTOs.Groups.Request;
using CRM.Application.Common.DTOs.Groups.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Groups.Commands.CreateGroup;

public record CreateGroupCommand(CreateGroupRequest Request) : IRequest<Result<GroupResponse>>;