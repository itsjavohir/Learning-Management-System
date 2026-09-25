using CRM.Application.Common.DTOs.GroupStudents.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.GroupStudents.Queries.GetGroupStudents;

public record GetGroupStudentsQuery(Guid GroupId, bool IncludeInactive)
    : IRequest<Result<List<GroupStudentResponse>>>;
