using CRM.Application.Common.DTOs.Users.Request;
using CRM.Application.Features.Users.Commands.CreateUser;
using CRM.Application.Features.Users.Queries.GetAllUsers;
using CRM.Application.Features.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IMediator mediator) : BaseController
{
    [HttpPost]

    public async Task<IActionResult> Create ([FromBody]CreateUserRequest request,CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateUserCommand(request),cancellationToken);
        if (!result.IsSuccess)
        {
            return HandleError(result);

        }

        return Ok(result);
    }
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