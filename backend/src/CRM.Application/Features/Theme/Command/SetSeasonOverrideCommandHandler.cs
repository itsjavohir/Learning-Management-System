using CRM.Application.Common.Wrappers;
using CRM.Application.Interfaces.Repositories;
using CRM.Domain.Entities;
using MediatR;

namespace CRM.Application.Features.Theme.Commands.SetSeasonOverrideCommand;

public class SetSeasonOverrideCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<SetSeasonOverrideCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(SetSeasonOverrideCommand command, CancellationToken cancellationToken)
    {
        var settings = await unitOfWork.ThemeSettings.GetAsync(cancellationToken);

        if (settings is null)
        {
            settings = ThemeSettings.CreateDefault();
            await unitOfWork.ThemeSettings.AddAsync(settings, cancellationToken);
        }

        if (command.Season is null)
            settings.ClearOverride(command.RequestedByUserId);
        else
            settings.SetOverride(command.Season.Value, command.RequestedByUserId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<bool>.Ok(true);
    }
}