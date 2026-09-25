using CRM.Application.Common.DTOs.Theme.Response;
using CRM.Application.Common.Wrappers;
using MediatR;

namespace CRM.Application.Features.Theme.Queries.GetSeasonHistory;

public record GetSeasonHistoryQuery : IRequest<Result<List<SeasonHistoryResponse>>>;