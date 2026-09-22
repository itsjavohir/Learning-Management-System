using CRM.Application.Common.Wrappers;
using CRM.Domain.Enums;
using MediatR;

namespace CRM.Application.Features.Theme.Commands.SetSeasonOverrideCommand;

public record SetSeasonOverrideCommand(
    Season? Season,
    Guid RequestedByUserId
) : IRequest<Result<bool>>;