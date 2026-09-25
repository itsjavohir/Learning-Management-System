using CRM.Application.Common.DTOs.GroupStudents.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.GroupStudents.Commands.TransferStudent;

public class TransferStudentCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<TransferStudentCommand, Result<GroupStudentResponse>>
{
    public async Task<Result<GroupStudentResponse>> Handle(TransferStudentCommand command, CancellationToken cancellationToken)
    {
        var currentGroupStudent = await unitOfWork.GroupStudent.GetByIdAsync(command.GroupStudentId, cancellationToken);

        if (currentGroupStudent is null)
        {
            return Result<GroupStudentResponse>.Fail("Group-student record not found", ErrorType.NotFound);
        }

        if (!currentGroupStudent.IsActive)
        {
            return Result<GroupStudentResponse>.Fail("Student is not active in this group", ErrorType.Conflict);
        }

        if (currentGroupStudent.GroupId == command.Request.ToGroupId)
        {
            return Result<GroupStudentResponse>.Fail("Student is already in the target group", ErrorType.Conflict);
        }

        var targetGroup = await unitOfWork.Group.GetByIdAsync(command.Request.ToGroupId, cancellationToken);
        if (targetGroup is null)
        {
            return Result<GroupStudentResponse>.Fail("Target group not found", ErrorType.NotFound);
        }

        var existingInTarget = await unitOfWork.GroupStudent.GetByGroupAndStudentAsync(
            command.Request.ToGroupId, currentGroupStudent.StudentId, cancellationToken);

        if (existingInTarget is not null && existingInTarget.IsActive)
        {
            return Result<GroupStudentResponse>.Fail("Student is already in the target group", ErrorType.Conflict);
        }

        var activeCount = await unitOfWork.GroupStudent.CountActiveByGroupIdAsync(command.Request.ToGroupId, cancellationToken);
        if (activeCount >= targetGroup.MaxStudents)
        {
            return Result<GroupStudentResponse>.Fail("Target group is full", ErrorType.Conflict);
        }

        currentGroupStudent.IsActive = false;
        currentGroupStudent.LeftAt = DateTime.UtcNow;
        currentGroupStudent.RemoveReason = "Transferred to another group";
        unitOfWork.GroupStudent.Update(currentGroupStudent);

        GroupStudent newGroupStudent;

        if (existingInTarget is not null)
        {
            existingInTarget.IsActive = true;
            existingInTarget.JoinedAt = DateTime.UtcNow;
            existingInTarget.LeftAt = null;
            existingInTarget.RemoveReason = null;
            existingInTarget.TransferredFromGroupStudentId = currentGroupStudent.Id;
            unitOfWork.GroupStudent.Update(existingInTarget);
            newGroupStudent = existingInTarget;
        }
        else
        {
            newGroupStudent = new GroupStudent
            {
                GroupId = command.Request.ToGroupId,
                StudentId = currentGroupStudent.StudentId,
                TransferredFromGroupStudentId = currentGroupStudent.Id
            };
            await unitOfWork.GroupStudent.AddAsync(newGroupStudent, cancellationToken);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        currentGroupStudent.TransferredToGroupStudentId = newGroupStudent.Id;
        unitOfWork.GroupStudent.Update(currentGroupStudent);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var student = currentGroupStudent.Student;

        var response = new GroupStudentResponse(
            newGroupStudent.Id,
            targetGroup.Id,
            targetGroup.Name,
            newGroupStudent.StudentId,
            student.User.FullName,
            newGroupStudent.JoinedAt,
            newGroupStudent.LeftAt,
            newGroupStudent.IsActive,
            newGroupStudent.RemoveReason,
            newGroupStudent.TransferredFromGroupStudentId,
            newGroupStudent.TransferredToGroupStudentId
        );

        return Result<GroupStudentResponse>.Ok(response);
    }
}
