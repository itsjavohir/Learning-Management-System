using CRM.Application.Common.DTOs.GroupStudents.Request;
using FluentValidation;

namespace CRM.Application.Features.GroupStudents.Commands.AddStudentToGroup;

public class AddStudentToGroupCommandValidator : AbstractValidator<AddStudentToGroupCommand>
{
    public AddStudentToGroupCommandValidator()
    {
        RuleFor(x => x.GroupId).NotEmpty();
        RuleFor(x => x.Request).SetValidator(new AddStudentToGroupRequestValidator());
    }
}

public class AddStudentToGroupRequestValidator : AbstractValidator<AddStudentToGroupRequest>
{
    public AddStudentToGroupRequestValidator()
    {
        RuleFor(x => x.StudentId).NotEmpty();
    }
}
