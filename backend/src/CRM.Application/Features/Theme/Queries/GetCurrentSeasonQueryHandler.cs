using CRM.Application.Common.DTOs.Theme.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Application.Interfaces.Services;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace CRM.Application.Features.Theme.Queries.GetCurrentSeasonQuery;

public class GetCurrentSeasonQueryHandler(
    IUnitOfWork unitOfWork,
    ISeasonalCalendarService seasonalCalendarService,
    IDateTimeProvider dateTimeProvider,
    IMemoryCache cache)
    : IRequestHandler<GetCurrentSeasonQuery, Result<SeasonResponse>>
{
    private const string CacheKey = "theme:current-season";

    public async Task<Result<SeasonResponse>> Handle(GetCurrentSeasonQuery query, CancellationToken cancellationToken)
    {
        var cached = await cache.GetOrCreateAsync(CacheKey, async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
            var now = dateTimeProvider.UtcNow;
            var seasonOverride = await unitOfWork.SeasonOverride.GetActiveAsync(cancellationToken);

            if (seasonOverride?.Season is not null)
            {
                return new SeasonResponse(
                    seasonOverride.Season.Value,
                    seasonalCalendarService.GetSeasonState(now).Event,
                    0d,
                    true);
            }

            var state = seasonalCalendarService.GetSeasonState(now);
            return new SeasonResponse(state.Season, state.Event, state.Transition, false);
        });

        return Result<SeasonResponse>.Ok(cached!);
    }
}