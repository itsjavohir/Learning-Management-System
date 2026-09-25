using CRM.Application.Common.DTOs.GroupStudents.Request;
using CRM.Application.Common.DTOs.GroupStudents.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.GroupStudents.Commands.AddStudentToGroup;

public record AddStudentToGroupCommand(Guid GroupId, AddStudentToGroupRequest Request)
    : IRequest<Result<GroupStudentResponse>>;
