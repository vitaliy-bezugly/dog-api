using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Dogs.Queries.GetDogQuery;

public class GetDogQueryHandler : IRequestHandler<GetDogQuery, Dog>
{
    private readonly IApplicationDbContext _context;

    public GetDogQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Dog> Handle(GetDogQuery request, CancellationToken cancellationToken)
    {
        Dog? dog = await _context.Dogs.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);
        if (dog is null)
        {
            throw new DogNotFoundException(request.Name);
        }
        
        return dog;
    }
}