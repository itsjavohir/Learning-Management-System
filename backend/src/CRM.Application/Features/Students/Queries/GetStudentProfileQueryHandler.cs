using CRM.Application.Common.DTOs.Students.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Students.Queries;

public class GetStudentProfileQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetStudentProfileQuery, Result<StudentProfileResponse>>
{
    public async Task<Result<StudentProfileResponse>> Handle(GetStudentProfileQuery query, CancellationToken cancellationToken)
    {
        var student = await unitOfWork.Student.GetByUserIdAsync(query.UserId, cancellationToken);

        if (student is null)
        {
            return Result<StudentProfileResponse>.Fail("Student not found", ErrorType.NotFound);
        }

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
