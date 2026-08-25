using CRM.Application.Common.DTOs.Users;
using CRM.Application.Common.DTOs.Users.Response;
using CRM.Domain.Entities;
using MediatR;

namespace CRM.Application.Features.Users.Queries.GetAllUsers;

public record GetAllUsersQuery: IRequest<List<UserResponse>>;
