using CRM.Application.Common.DTOs.Theme.Response;
using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Application.Interfaces.Services;
using MediatR;

namespace CRM.Application.Features.Theme.Queries.GetCurrentSeasonQuery;

public class GetCurrentSeasonQueryHandler(
    IUnitOfWork unitOfWork,
    ISeasonCalculator seasonCalculator,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<GetCurrentSeasonQuery, Result<SeasonResponse>>
{
    public async Task<Result<SeasonResponse>> Handle(GetCurrentSeasonQuery query, CancellationToken cancellationToken)
    {
        var settings = await unitOfWork.ThemeSettings.GetAsync(cancellationToken);

        if (settings is not null && settings.IsOverrideEnabled && settings.ManualOverride is not null)
        {
            var overrideResponse = new SeasonResponse(
                Season: settings.ManualOverride.Value,
                IsManualOverride: true);

            return Result<SeasonResponse>.Ok(overrideResponse);
        }

        var season = seasonCalculator.GetSeasonByDate(dateTimeProvider.UtcNow);

        var response = new SeasonResponse(
            Season: season,
            IsManualOverride: false);

        return Result<SeasonResponse>.Ok(response);
    }
}