using CRM.Application.Common.DTOs.Lessons.Response;
using CRM.Application.Common.Extensions;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Lessons.Commands.UpdateLesson;

public class UpdateLessonCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateLessonCommand, Result<LessonResponse>>
{
    public async Task<Result<LessonResponse>> Handle(UpdateLessonCommand command, CancellationToken cancellationToken)
    {
        var lesson = await unitOfWork.Lesson.GetByIdAsync(command.Id, cancellationToken);
        if (lesson is null)
        {
            return Result<LessonResponse>.Fail("Lesson not found", ErrorType.NotFound);
        }

        var request = command.Request;

        lesson.WeekNumber = request.WeekNumber;
        lesson.LessonDate = request.LessonDate.ToUtc();
        lesson.Title = request.Title;
        lesson.Description = request.Description;
        lesson.HomeworkDescription = request.HomeworkDescription;
        lesson.MaterialUrl = request.MaterialUrl;
        lesson.IsCompleted = request.IsCompleted;

        unitOfWork.Lesson.Update(lesson);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new LessonResponse(
            lesson.Id,
            lesson.GroupId,
            lesson.Group.Name,
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
