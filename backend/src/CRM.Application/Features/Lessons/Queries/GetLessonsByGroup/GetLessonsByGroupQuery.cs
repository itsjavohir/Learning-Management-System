using CRM.Application.Common.DTOs.Lessons.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Lessons.Queries.GetLessonsByGroup;

public record GetLessonsByGroupQuery(Guid GroupId) : IRequest<Result<List<LessonResponse>>>;
