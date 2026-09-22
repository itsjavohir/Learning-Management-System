using CRM.Domain.Enums;

namespace CRM.Application.Interfaces.Services;

public interface ISeasonCalculator
{
    Season GetSeasonByDate(DateTime date);
}