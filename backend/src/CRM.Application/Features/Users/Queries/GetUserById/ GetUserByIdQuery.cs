using CRM.Application.Common.DTOs.Users;
using CRM.Domain.Entities;
using MediatR;

namespace CRM.Application.Features.Users.Queries.GetUserById;

public record  GetUserByIdQuery (Guid Id) : IRequest <UserDto?>;

