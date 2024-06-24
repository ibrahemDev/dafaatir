using Dafaatir.Modules.Users.Contracts.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Dafaatir.Modules.Users.Api.Controllers;
//[Authorize(Policy)]
internal class UsersController : BaseController
{
    //private const string Policy = "users";


    private readonly IMediator _mediator;
    private readonly ILogger<UsersController> _logger;
    public UsersController(IMediator mediator, ILogger<UsersController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }



    [HttpGet("")]
    [SwaggerOperation("Get user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> GetAsync()
    {
        GetAllUsersQuery getAllUsersQuery = new GetAllUsersQuery();
        Console.WriteLine("qqqqqqqqqqqqqqqqqqqqqqqqqqqq");
        var users = await _mediator.Send(getAllUsersQuery);

        await Task.Delay(5000);
        return Ok(users);
    }
}