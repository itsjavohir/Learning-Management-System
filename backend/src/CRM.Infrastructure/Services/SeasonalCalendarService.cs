using CRM.Application.Interfaces.Services;
using CRM.Domain.Enums;

namespace CRM.Infrastructure.Services;

public class SeasonalCalendarService : ISeasonalCalendarService
{
    public (Season Season, SeasonalEvent Event, double Transition) GetSeasonState(DateTime dateUtc)
    {
        var date = dateUtc.Date;
        var season = GetSeason(date);
        var seasonStart = GetSeasonStart(date, season);
        var nextSeasonStart = GetNextSeasonStart(seasonStart);
        var totalDays = (nextSeasonStart - seasonStart).TotalDays;
        var elapsedDays = (date - seasonStart).TotalDays;
        var transition = Math.Clamp(elapsedDays / totalDays, 0d, 1d);
        var seasonalEvent = IsNewYearWindow(date) ? SeasonalEvent.NewYear : SeasonalEvent.None;

        return (season, seasonalEvent, transition);
    }

    private static Season GetSeason(DateTime date) => date.Month switch
    {
        12 or 1 or 2 => Season.Winter,
        3 or 4 or 5 => Season.Spring,
        6 or 7 or 8 => Season.Summer,
        _ => Season.Autumn
    };

    private static DateTime GetSeasonStart(DateTime date, Season season)
    {
        var year = date.Year;
        return season switch
        {
            Season.Winter when date.Month == 12 => new DateTime(year, 12, 1),
            Season.Winter => new DateTime(year, 1, 1),
            Season.Spring => new DateTime(year, 3, 1),
            Season.Summer => new DateTime(year, 6, 1),
            _ => new DateTime(year, 9, 1)
        };
    }

    private static DateTime GetNextSeasonStart(DateTime seasonStart) => seasonStart.Month switch
    {
        1 => new DateTime(seasonStart.Year, 3, 1),
        3 => new DateTime(seasonStart.Year, 6, 1),
        6 => new DateTime(seasonStart.Year, 9, 1),
        9 => new DateTime(seasonStart.Year, 12, 1),
        _ => new DateTime(seasonStart.Year + 1, 3, 1)
    };

    private static bool IsNewYearWindow(DateTime date)
        => date.Month == 12 && date.Day >= 20 || date.Month == 1 && date.Day <= 10;
}