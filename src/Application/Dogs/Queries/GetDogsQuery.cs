using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Dogs.Queries;

public record GetDogsQuery : IRequest<PaginatedList<Dog>>
{
    private const int MaxPageSize = 50;
    
    public int PageNumber { get; init; }
    public int PageSize { get; init; }

    public GetDogsQuery()
    {
        PageNumber = 1;
        PageSize = MaxPageSize;
    }

    public GetDogsQuery(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize > MaxPageSize ? MaxPageSize : pageSize;
    }
}