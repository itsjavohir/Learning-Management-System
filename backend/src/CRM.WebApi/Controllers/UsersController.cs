using CRM.Application.Features.Users.Queries.GetAllUsers;
using CRM.Application.Features.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllUsersQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetUserByIdQuery(id), cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }
}