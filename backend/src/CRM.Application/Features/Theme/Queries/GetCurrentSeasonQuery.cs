using CRM.Application.Common.DTOs.Theme.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Theme.Queries.GetCurrentSeasonQuery;

public record GetCurrentSeasonQuery : IRequest<Result<SeasonResponse>>;