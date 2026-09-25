using CRM.Application.Common.DTOs.Students.Response;
using CRM.Application.Common.Extensions;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Students.Commands.UpdateStudentProfile;

public class UpdateStudentProfileCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateStudentProfileCommand, Result<StudentProfileResponse>>
{
    public async Task<Result<StudentProfileResponse>> Handle(UpdateStudentProfileCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var student = await unitOfWork.Student.GetByUserIdAsync(command.UserId, cancellationToken);

        if (student is null)
        {
            return Result<StudentProfileResponse>.Fail("Student not found", ErrorType.NotFound);
        }

        student.PhotoUrl = request.PhotoUrl;
        student.DateOfBirth = request.DateOfBirth?.ToUtc();
        student.TelegramUsername = request.TelegramUsername;
        student.GithubUrl = request.GithubUrl;
        student.AboutMe = request.AboutMe;

        unitOfWork.Student.Update(student);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new StudentProfileResponse(
            student.User.Id,
            student.User.FirstName,
            student.User.LastName,
            student.User.PhoneNumber,
            student.User.Email,
            student.Balance,
            student.PhotoUrl,
            student.DateOfBirth,
            student.TelegramUsername,
            student.GithubUrl,
            student.AboutMe,
            student.IsActive,
            student.EnrollDate
        );

        return Result<StudentProfileResponse>.Ok(response);
    }
}
