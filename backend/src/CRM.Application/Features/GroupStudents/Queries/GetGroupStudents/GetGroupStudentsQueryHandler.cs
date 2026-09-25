using CRM.Application.Common.DTOs.GroupStudents.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.GroupStudents.Queries.GetGroupStudents;

public class GetGroupStudentsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetGroupStudentsQuery, Result<List<GroupStudentResponse>>>
{
    public async Task<Result<List<GroupStudentResponse>>> Handle(GetGroupStudentsQuery query, CancellationToken cancellationToken)
    {
        var group = await unitOfWork.Group.GetByIdAsync(query.GroupId, cancellationToken);
        if (group is null)
        {
            return Result<List<GroupStudentResponse>>.Fail("Group not found", ErrorType.NotFound);
        }

        var groupStudents = await unitOfWork.GroupStudent.GetByGroupIdAsync(query.GroupId, query.IncludeInactive, cancellationToken);

        var response = groupStudents.Select(gs => new GroupStudentResponse(
            gs.Id,
            gs.GroupId,
            gs.Group.Name,
            gs.StudentId,
            gs.Student.User.FullName,
            gs.JoinedAt,
            gs.LeftAt,
            gs.IsActive,
            gs.RemoveReason,
            gs.TransferredFromGroupStudentId,
            gs.TransferredToGroupStudentId
        )).ToList();

        return Result<List<GroupStudentResponse>>.Ok(response);
    }
}
