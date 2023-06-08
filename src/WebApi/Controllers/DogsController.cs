using Application.Dogs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Contracts.Queries;

namespace WebApi.Controllers;

public class DogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DogsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet, Route(ApiRoutes.Dogs.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, [FromQuery] SortingQuery sortingQuery)
    {
        var query = new GetDogsQuery(paginationQuery.PageNumber, paginationQuery.PageSize, sortingQuery.Attribute, sortingQuery.Order);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}