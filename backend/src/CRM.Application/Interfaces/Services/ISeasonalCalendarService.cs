using CRM.Domain.Enums;

namespace CRM.Application.Interfaces.Services;

public interface ISeasonalCalendarService
{
    (Season Season, SeasonalEvent Event, double Transition) GetSeasonState(DateTime dateUtc);
}