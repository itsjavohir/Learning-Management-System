using CRM.Application.Common.DTOs.Groups.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Groups.Queries.GetGroupById;

public class GetGroupByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetGroupByIdQuery, Result<GroupResponse>>
{
    public async Task<Result<GroupResponse>> Handle(GetGroupByIdQuery query, CancellationToken cancellationToken)
    {
        var group = await unitOfWork.Group.GetByIdAsync(query.Id, cancellationToken);

        if (group is null)
        {
            return Result<GroupResponse>.Fail("Group not found", ErrorType.NotFound);
        }

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