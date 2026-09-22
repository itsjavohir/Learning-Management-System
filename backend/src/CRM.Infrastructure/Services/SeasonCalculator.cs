using CRM.Application.Interfaces.Services;
using CRM.Domain.Enums;

namespace CRM.Infrastructure.Services;

public class SeasonCalculator : ISeasonCalculator
{
    public Season GetSeasonByDate(DateTime date) => date.Month switch
    {
        12 or 1 or 2 => Season.Winter,
        3 or 4 or 5 => Season.Spring,
        6 or 7 or 8 => Season.Summer,
        9 or 10 or 11 => Season.Autumn,
        _ => throw new ArgumentOutOfRangeException(nameof(date))
    };
}