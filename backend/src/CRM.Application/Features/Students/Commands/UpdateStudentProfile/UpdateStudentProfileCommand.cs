using CRM.Application.Common.DTOs.Students.Request;
using CRM.Application.Common.DTOs.Students.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Students.Commands.UpdateStudentProfile;

public record UpdateStudentProfileCommand(Guid UserId, UpdateStudentProfileRequest Request)
    : IRequest<Result<StudentProfileResponse>>;
