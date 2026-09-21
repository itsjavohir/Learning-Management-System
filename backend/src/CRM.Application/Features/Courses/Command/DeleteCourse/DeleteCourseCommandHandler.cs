using CRM.Application.Common.Wrappers;
using CRM.Application.Features.Courses.Command.DeleteCourse;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Courses.Command.DeleteCourse;

public class DeleteCourseCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCourseCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(
        DeleteCourseCommand command,
        CancellationToken cancellationToken)
    {
        var course = await unitOfWork.Course.GetByIdAsync(
            command.Request.Id,
            cancellationToken);

        if (course is null)
        {
            return Result<bool>.Fail("Course not found !",ErrorType.NotFound);
        }

        unitOfWork.Course.Delete(course);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true);
    }
}

