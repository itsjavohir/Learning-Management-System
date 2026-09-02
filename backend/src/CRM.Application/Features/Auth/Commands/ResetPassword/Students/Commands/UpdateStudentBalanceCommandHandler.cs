using CRM.Application.Common.DTOs.Students.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Students.Commands.UpdateStudentBalance;

public class UpdateStudentBalanceCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateStudentBalanceCommand, Result<StudentProfileResponse>>
{
    public async Task<Result<StudentProfileResponse>> Handle(UpdateStudentBalanceCommand command, CancellationToken cancellationToken)
    {
        var request = command.Request;

        var student = await unitOfWork.Student.GetByIdAsync(command.StudentId, cancellationToken);

        if (student is null)
        {
            return Result<StudentProfileResponse>.Fail("Student not found", ErrorType.NotFound);
        }

        student.Balance += request.Amount;

        unitOfWork.Student.Update(student);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new StudentProfileResponse(
            student.User.Id,
            student.User.FirstName,
            student.User.LastName,
            student.User.PhoneNumber,
            student.User.Email,
            student.Balance
        );

        return Result<StudentProfileResponse>.Ok(response);
    }
}