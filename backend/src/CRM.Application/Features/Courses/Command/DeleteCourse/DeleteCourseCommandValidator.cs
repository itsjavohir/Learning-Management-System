using CRM.Application.Common.DTOs.Course.Request;
using FluentValidation;

namespace CRM.Application.Features.Courses.Command.DeleteCourse;

public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
{
    public DeleteCourseCommandValidator()
    {
        RuleFor(x => x.Request).SetValidator(new DeleteCourseRequestValidator());
    }
}

public class DeleteCourseRequestValidator : AbstractValidator<DeleteCourseRequest>
{
    public DeleteCourseRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
