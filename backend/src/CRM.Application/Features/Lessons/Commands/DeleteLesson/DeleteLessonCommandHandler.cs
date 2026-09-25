using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Lessons.Commands.DeleteLesson;

public class DeleteLessonCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteLessonCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(DeleteLessonCommand command, CancellationToken cancellationToken)
    {
        var lesson = await unitOfWork.Lesson.GetByIdAsync(command.Id, cancellationToken);
        if (lesson is null)
        {
            return Result<bool>.Fail("Lesson not found", ErrorType.NotFound);
        }

        unitOfWork.Lesson.Delete(lesson);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true);
    }
}
