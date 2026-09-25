using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CRM.Application.Features.Theme.Commands.SetSeasonOverrideCommand;

public class SetSeasonOverrideCommandHandler(
    IUnitOfWork unitOfWork,
    IMemoryCache cache,
    ILogger<SetSeasonOverrideCommandHandler> logger)
    : IRequestHandler<SetSeasonOverrideCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(SetSeasonOverrideCommand command, CancellationToken cancellationToken)
    {
        DateTime? expiresAtUtc = command.Season is null ? null : DateTime.UtcNow.AddDays(30);
        var seasonOverride = SeasonOverride.Create(command.Season, command.RequestedByUserId, expiresAtUtc);

        await unitOfWork.SeasonOverride.AddAsync(seasonOverride, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        cache.Remove("theme:current-season");
        logger.LogInformation(
            "Season override changed by user {UserId}: {Season}, expires at {ExpiresAtUtc}",
            command.RequestedByUserId,
            command.Season,
            expiresAtUtc);

        return Result<bool>.Ok(true);
    }
}