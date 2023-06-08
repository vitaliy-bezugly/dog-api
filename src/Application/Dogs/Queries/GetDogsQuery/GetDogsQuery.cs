using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Dogs.Queries.GetDogsQuery;

public record GetDogsQuery : IRequest<PaginatedList<Dog>>
{
    private const int MaxPageSize = 50;
    
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public string Attribute { get; init; }
    public string Order { get; init; }
    
    public GetDogsQuery(int pageNumber, int pageSize, string attribute, string order)
    {
        PageNumber = pageNumber;
        Attribute = attribute;
        Order = order;
        PageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
    }
}