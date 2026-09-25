using CRM.Application.Common.DTOs.Lessons.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Lessons.Queries.GetLessonsByGroup;

public class GetLessonsByGroupQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetLessonsByGroupQuery, Result<List<LessonResponse>>>
{
    public async Task<Result<List<LessonResponse>>> Handle(GetLessonsByGroupQuery query, CancellationToken cancellationToken)
    {
        var group = await unitOfWork.Group.GetByIdAsync(query.GroupId, cancellationToken);
        if (group is null)
        {
            return Result<List<LessonResponse>>.Fail("Group not found", ErrorType.NotFound);
        }

        var lessons = await unitOfWork.Lesson.GetByGroupIdAsync(query.GroupId, cancellationToken);

        var response = lessons.Select(l => new LessonResponse(
            l.Id,
            l.GroupId,
            group.Name,
            l.WeekNumber,
            l.LessonDate,
            l.Title,
            l.Description,
            l.HomeworkDescription,
            l.MaterialUrl,
            l.IsCompleted
        )).ToList();

        return Result<List<LessonResponse>>.Ok(response);
    }
}
