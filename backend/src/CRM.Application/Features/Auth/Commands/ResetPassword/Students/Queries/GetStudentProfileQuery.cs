using CRM.Application.Common.DTOs.Students.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Students.Queries;

public class GetStudentProfileQuery(Guid Userid):IRequest<Result<StudentProfileResponse>>;
