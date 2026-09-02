using CRM.Application.Common.DTOs.Login.Request;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Auth.Commands.ForgotPassword;

public record ForgotPasswordCommand(ForgotPasswordRequest Request):IRequest<Result<bool>>;
