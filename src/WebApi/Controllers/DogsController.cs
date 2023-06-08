using Application.Dogs.Commands.CreateDogCommand;
using Application.Dogs.Queries.GetDogByNameQuery;
using Application.Dogs.Queries.GetDogsQuery;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Contracts;
using WebApi.Contracts.Models;
using WebApi.Contracts.Queries;
using WebApi.Contracts.Requests;
using WebApi.Extensions;

namespace WebApi.Controllers;

public class DogsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public DogsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet, Route(ApiRoutes.Dogs.GetByName)]
    public async Task<IActionResult> GetByName(string name)
    {
        var query = new GetDogByNameQuery(name);
        var dog = await _mediator.Send(query);
        return Ok(_mapper.Map<DogViewModel>(dog));
    }

    [HttpGet, Route(ApiRoutes.Dogs.GetAll)]
    public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, [FromQuery] SortingQuery sortingQuery)
    {
        var query = new GetDogsQuery(paginationQuery.PageNumber, paginationQuery.PageSize, sortingQuery.Attribute, sortingQuery.Order);
        var result = await _mediator.Send(query);
        return Ok(_mapper.ToPaginationListViewModel(result));
    }
    
    [HttpPost, Route(ApiRoutes.Dogs.Create)]
    public async Task<IActionResult> Create([FromBody] CreateDogRequest request)
    {
        var command = new CreateDogCommand(request.Name, request.Color, request.TailLength, request.Weight);
        await _mediator.Send(command);
        
        // var response = new CreatedDogResponse { Name = request.Name, Color = request.Color, TailLength = request.TailLength, Weight = request.Weight};
        var response = _mapper.Map<DogViewModel>(request);
        var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
        var locationUrl = $"{baseUrl}/{ApiRoutes.Dogs.GetByName.Replace("{name}", response.Name)}";
        
        return Created(locationUrl, response);
    }
}