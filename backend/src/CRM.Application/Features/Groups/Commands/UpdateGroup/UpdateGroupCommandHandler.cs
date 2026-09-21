using CRM.Application.Common.DTOs.Groups.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Groups.Commands.UpdateGroup;

public class UpdateGroupCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateGroupCommand, Result<GroupResponse>>
{
    public async Task<Result<GroupResponse>> Handle(UpdateGroupCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var group = await unitOfWork.Group.GetByIdAsync(command.Id, cancellationToken);
        if (group is null)
        {
            return Result<GroupResponse>.Fail("Group not found", ErrorType.NotFound);
        }

        group.Name = request.Name;
        group.StartDate = request.StartDate;
        group.MaxStudents = request.MaxStudents;

        unitOfWork.Group.Update(group);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new GroupResponse(
            group.Id,
            group.Name,
            group.Course.Id,
            group.Course.Name,
            group.Mentor.Id,
            group.Mentor.User.FullName,
            group.StartDate,
            group.MaxStudents
        );

        return Result<GroupResponse>.Ok(response);
    }
}