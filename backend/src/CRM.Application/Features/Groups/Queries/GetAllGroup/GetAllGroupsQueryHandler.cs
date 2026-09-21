using CRM.Application.Common.DTOs.Groups.Response;
using CRM.Application.Interfaces.Repositories;
using MediatR;

namespace CRM.Application.Features.Groups.Queries.GetAllGroups;

public class GetAllGroupsQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllGroupsQuery, List<GroupResponse>>
{
    public async Task<List<GroupResponse>> Handle(GetAllGroupsQuery query, CancellationToken cancellationToken)
    {
        var groups = await unitOfWork.Group.GetAllAsync(cancellationToken);

        return groups.Select(g => new GroupResponse(
            g.Id,
            g.Name,
            g.Course.Id,
            g.Course.Name,
            g.Mentor.Id,
            g.Mentor.User.FullName,
            g.StartDate,
            g.MaxStudents
        )).ToList();
    }
}