using CRM.Application.Common.DTOs.Users.Request;
using CRM.Application.Common.DTOs.Users.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(Guid Id,UpdateUserRequest Request): IRequest<Result<UpdateUserResponse>>;

