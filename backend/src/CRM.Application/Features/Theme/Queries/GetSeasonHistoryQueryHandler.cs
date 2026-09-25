using CRM.Application.Common.DTOs.Theme.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using MediatR;

namespace CRM.Application.Features.Theme.Queries.GetSeasonHistory;

public class GetSeasonHistoryQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetSeasonHistoryQuery, Result<List<SeasonHistoryResponse>>>
{
    public async Task<Result<List<SeasonHistoryResponse>>> Handle(
        GetSeasonHistoryQuery query,
        CancellationToken cancellationToken)
    {
        var history = await unitOfWork.SeasonOverride.GetHistoryAsync(cancellationToken);

        return Result<List<SeasonHistoryResponse>>.Ok(history.Select(item =>
            new SeasonHistoryResponse(
                item.Id,
                item.Season,
                item.SetByUserId,
                item.SetAtUtc,
                item.ExpiresAtUtc)).ToList());
    }
}