using CRM.Application.Common.DTOs.Login.Request;
using CRM.Application.Common.DTOs.Login.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Auth.Commands.ResetPassword;

public record ResetPasswordCommand(ResetPasswordRequest Request):IRequest<Result<LoginResponse>>;

