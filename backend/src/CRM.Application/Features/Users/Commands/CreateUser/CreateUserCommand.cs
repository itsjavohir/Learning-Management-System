using CRM.Application.Common.DTOs.Users.Request;
using CRM.Application.Common.DTOs.Users.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(CreateUserRequest Request) : IRequest<Result<CreateUserResponse>>;