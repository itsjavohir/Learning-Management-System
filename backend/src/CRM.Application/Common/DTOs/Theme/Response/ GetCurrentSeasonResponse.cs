using CRM.Domain.Enums;

namespace CRM.Application.Common.DTOs.Theme.Response;

public record SeasonResponse(
    Season Season,
    SeasonalEvent Event,
    double Transition,
    bool IsOverridden
);

public record SeasonHistoryResponse(
    Guid Id,
    Season? Season,
    Guid SetByUserId,
    DateTime SetAtUtc,
    DateTime? ExpiresAtUtc
);