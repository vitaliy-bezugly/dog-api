using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using MediatR;

namespace Application.Dogs.Queries;

public class GetDogsQueryHandler : IRequestHandler<GetDogsQuery, PaginatedList<Dog>>
{
    private readonly IApplicationDbContext _context;

    public GetDogsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<Dog>> Handle(GetDogsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Dog> source = _context.Dogs.AsQueryable();
        IQueryable<Dog> sortedQuery = DogSortingQueryBuilder
            .TryBuildSortingQueryIfInvalidReturnSource(source, request.Attribute, request.Order);
        
        PaginatedList<Dog> paginatedList = await PaginatedList<Dog>.CreateAsync(sortedQuery, request.PageNumber, request.PageSize);
        return paginatedList;
    }
}