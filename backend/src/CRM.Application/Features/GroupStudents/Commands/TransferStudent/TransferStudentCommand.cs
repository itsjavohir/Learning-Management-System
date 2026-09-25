using CRM.Application.Common.DTOs.GroupStudents.Request;
using CRM.Application.Common.DTOs.GroupStudents.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.GroupStudents.Commands.TransferStudent;

public record TransferStudentCommand(Guid GroupStudentId, TransferStudentRequest Request)
    : IRequest<Result<GroupStudentResponse>>;
