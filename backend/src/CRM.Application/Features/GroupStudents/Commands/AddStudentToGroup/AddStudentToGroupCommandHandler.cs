using CRM.Application.Common.DTOs.GroupStudents.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.GroupStudents.Commands.AddStudentToGroup;

public class AddStudentToGroupCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddStudentToGroupCommand, Result<GroupStudentResponse>>
{
    public async Task<Result<GroupStudentResponse>> Handle(AddStudentToGroupCommand command, CancellationToken cancellationToken)
    {
        var group = await unitOfWork.Group.GetByIdAsync(command.GroupId, cancellationToken);
        if (group is null)
        {
            return Result<GroupStudentResponse>.Fail("Group not found", ErrorType.NotFound);
        }

        var student = await unitOfWork.Student.GetByIdAsync(command.Request.StudentId, cancellationToken);
        if (student is null)
        {
            return Result<GroupStudentResponse>.Fail("Student not found", ErrorType.NotFound);
        }

        var existing = await unitOfWork.GroupStudent.GetByGroupAndStudentAsync(
            command.GroupId, command.Request.StudentId, cancellationToken);

        if (existing is not null && existing.IsActive)
        {
            return Result<GroupStudentResponse>.Fail("Student is already in this group", ErrorType.Conflict);
        }

        var activeCount = await unitOfWork.GroupStudent.CountActiveByGroupIdAsync(command.GroupId, cancellationToken);
        if (activeCount >= group.MaxStudents)
        {
            return Result<GroupStudentResponse>.Fail("Group is full", ErrorType.Conflict);
        }

        GroupStudent groupStudent;

        if (existing is not null)
        {
            existing.IsActive = true;
            existing.JoinedAt = DateTime.UtcNow;
            existing.LeftAt = null;
            existing.RemoveReason = null;
            unitOfWork.GroupStudent.Update(existing);
            groupStudent = existing;
        }
        else
        {
            groupStudent = new GroupStudent
            {
                GroupId = command.GroupId,
                StudentId = command.Request.StudentId
            };
            await unitOfWork.GroupStudent.AddAsync(groupStudent, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new GroupStudentResponse(
            groupStudent.Id,
            group.Id,
            group.Name,
            student.Id,
            student.User.FullName,
            groupStudent.JoinedAt,
            groupStudent.LeftAt,
            groupStudent.IsActive,
            groupStudent.RemoveReason,
            groupStudent.TransferredFromGroupStudentId,
            groupStudent.TransferredToGroupStudentId
        );

        return Result<GroupStudentResponse>.Ok(response);
    }
}
