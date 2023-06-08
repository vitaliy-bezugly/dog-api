using Application.Dogs.Commands;
using Application.Dogs.Queries;
using Application.Dogs.Queries.GetDogQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Contracts.Queries;
using WebApi.Contracts.Requests;
using WebApi.Contracts.Responses;

namespace WebApi.Controllers;

public class DogsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DogsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet, Route(ApiRoutes.Dogs.GetByName)]
    public async Task<IActionResult> GetByName(string name)
    {
        var query = new GetDogQuery(name);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet, Route(ApiRoutes.Dogs.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, [FromQuery] SortingQuery sortingQuery)
    {
        var query = new GetDogsQuery(paginationQuery.PageNumber, paginationQuery.PageSize, sortingQuery.Attribute, sortingQuery.Order);
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost, Route(ApiRoutes.Dogs.Create)]
    public async Task<IActionResult> Create([FromBody] CreateDogRequest request)
    {
        var command = new CreateDogCommand(request.Name, request.Color, request.TailLength, request.Weight);
        await _mediator.Send(command);
        
        var response = new CreatedDogResponse { Name = request.Name, Color = request.Color, TailLength = request.TailLength, Weight = request.Weight};
        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUrl = $"{baseUrl}/{ApiRoutes.Dogs.GetByName.Replace("{name}", response.Name)}";
        
        return Created(locationUrl, response);
    }
}