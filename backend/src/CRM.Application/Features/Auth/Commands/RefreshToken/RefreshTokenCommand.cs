using CRM.Application.Common.DTOs.Login.Request;
using CRM.Application.Common.DTOs.Login.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Auth.Commands.RefreshToken;

public record RefreshTokenCommand(RefreshTokenRequest Request) : IRequest<Result<LoginResponse>>;

