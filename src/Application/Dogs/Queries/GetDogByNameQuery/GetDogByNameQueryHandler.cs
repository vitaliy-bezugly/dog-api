using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dogs.Queries.GetDogByNameQuery;

public class GetDogByNameQueryHandler : IRequestHandler<GetDogByNameQuery, Dog>
{
    private readonly IApplicationDbContext _context;

    public GetDogByNameQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Dog> Handle(Queries.GetDogByNameQuery.GetDogByNameQuery request, CancellationToken cancellationToken)
    {
        Dog? dog = await _context.Dogs.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
        if (dog is null)
        {
            throw new DogNotFoundException(request.Name);
        }
        
        return dog;
    }
}