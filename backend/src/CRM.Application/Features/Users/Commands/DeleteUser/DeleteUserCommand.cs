using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest<Result<bool>>;