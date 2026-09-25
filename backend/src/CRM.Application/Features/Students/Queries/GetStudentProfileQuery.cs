using CRM.Application.Common.DTOs.Students.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Students.Queries;

public record GetStudentProfileQuery(Guid UserId) : IRequest<Result<StudentProfileResponse>>;
