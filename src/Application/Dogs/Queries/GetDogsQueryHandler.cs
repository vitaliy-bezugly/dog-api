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
        var source = _context.Dogs.AsQueryable();
        var paginatedList = await PaginatedList<Dog>.CreateAsync(source, request.PageNumber, request.PageSize);
        
        return paginatedList;
    }
}