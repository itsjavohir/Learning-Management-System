using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.GroupStudents.Commands.RemoveStudentFromGroup;

public class RemoveStudentFromGroupCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RemoveStudentFromGroupCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(RemoveStudentFromGroupCommand command, CancellationToken cancellationToken)
    {
        var groupStudent = await unitOfWork.GroupStudent.GetByIdAsync(command.GroupStudentId, cancellationToken);

        if (groupStudent is null)
        {
            return Result<bool>.Fail("Group-student record not found", ErrorType.NotFound);
        }

        if (!groupStudent.IsActive)
        {
            return Result<bool>.Fail("Student is already removed from this group", ErrorType.Conflict);
        }

        groupStudent.IsActive = false;
        groupStudent.LeftAt = DateTime.UtcNow;
        groupStudent.RemoveReason = command.Request.Reason;

        unitOfWork.GroupStudent.Update(groupStudent);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true);
    }
}
