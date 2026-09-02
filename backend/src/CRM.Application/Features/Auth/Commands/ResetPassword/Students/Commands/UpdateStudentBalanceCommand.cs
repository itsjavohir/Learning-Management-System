using CRM.Application.Common.DTOs.Students.Request;
using CRM.Application.Common.DTOs.Students.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Students.Commands.UpdateStudentBalance;

public record UpdateStudentBalanceCommand(Guid StudentId, UpdateStudentBalanceRequest Request) : IRequest<Result<StudentProfileResponse>>;