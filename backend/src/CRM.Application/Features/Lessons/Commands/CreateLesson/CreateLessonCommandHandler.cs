using CRM.Application.Common.DTOs.Lessons.Response;
using CRM.Application.Common.Extensions;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Lessons.Commands.CreateLesson;

public class CreateLessonCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateLessonCommand, Result<LessonResponse>>
{
    public async Task<Result<LessonResponse>> Handle(CreateLessonCommand command, CancellationToken cancellationToken)
    {
        var group = await unitOfWork.Group.GetByIdAsync(command.GroupId, cancellationToken);
        if (group is null)
        {
            return Result<LessonResponse>.Fail("Group not found", ErrorType.NotFound);
        }

        var request = command.Request;

        var lesson = new Lesson
        {
            GroupId = command.GroupId,
            WeekNumber = request.WeekNumber,
            LessonDate = request.LessonDate.ToUtc(),
            Title = request.Title,
            Description = request.Description,
            HomeworkDescription = request.HomeworkDescription,
            MaterialUrl = request.MaterialUrl
        };

        await unitOfWork.Lesson.AddAsync(lesson, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new LessonResponse(
            lesson.Id,
            group.Id,
            group.Name,
            lesson.WeekNumber,
            lesson.LessonDate,
            lesson.Title,
            lesson.Description,
            lesson.HomeworkDescription,
            lesson.MaterialUrl,
            lesson.IsCompleted
        );

        return Result<LessonResponse>.Ok(response);
    }
}
