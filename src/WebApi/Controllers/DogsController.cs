using Application.Dogs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;

namespace WebApi.Controllers;

public class DogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DogsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet, Route(ApiRoutes.Dogs.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetDogsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}