using CRM.Application.Common.DTOs.Groups.Response;
using CRM.Application.Common.Extensions;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Groups.Commands.CreateGroup;

public class CreateGroupCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateGroupCommand, Result<GroupResponse>>
{
    public async Task<Result<GroupResponse>> Handle(
        CreateGroupCommand command,
        CancellationToken cancellationToken)
    {
        var request = command.Request;

        var course = await unitOfWork.Course
            .GetByIdAsync(request.CourseId, cancellationToken);

        if (course is null)
        {
            return Result<GroupResponse>.Fail(
                "Course not found",
                ErrorType.NotFound);
        }

        var mentor = await unitOfWork.Mentor
            .GetByIdAsync(request.MentorId, cancellationToken);   

        if (mentor is null)
        {
            return Result<GroupResponse>.Fail(
                "Mentor not found",
                ErrorType.NotFound);
        }

        var group = new Group
        {
            Name = request.Name,
            CourseId = request.CourseId,
            MentorId = mentor.Id,

            StartDate = request.StartDate.ToUtc(),
            MaxStudents = request.MaxStudents
        };

        await unitOfWork.Group
            .AddAsync(group, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new GroupResponse(
            group.Id,
            group.Name,
            course.Id,
            course.Name,
            mentor.Id,
            mentor.User.FullName,
            group.StartDate,
            group.MaxStudents
        );

        return Result<GroupResponse>.Ok(response);
    }
}