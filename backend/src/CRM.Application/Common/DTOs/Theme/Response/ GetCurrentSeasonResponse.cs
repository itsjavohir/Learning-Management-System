using CRM.Domain.Enums;

namespace CRM.Application.Common.DTOs.Theme.Response;

public record SeasonResponse(
    Season Season,
    bool IsManualOverride
);