using CRM.Application.Common.DTOs.GroupStudents.Request;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.GroupStudents.Commands.RemoveStudentFromGroup;

public record RemoveStudentFromGroupCommand(Guid GroupStudentId, RemoveStudentFromGroupRequest Request)
    : IRequest<Result<bool>>;
