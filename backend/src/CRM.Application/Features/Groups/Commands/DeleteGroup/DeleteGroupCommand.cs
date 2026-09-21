using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Groups.Commands.DeleteGroup;

public record DeleteGroupCommand(Guid Id) : IRequest<Result<bool>>;