using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Dogs.Commands;

public class CreateDogCommandHandler : IRequestHandler<CreateDogCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<CreateDogCommandHandler> _logger;

    public CreateDogCommandHandler(IApplicationDbContext context, ILogger<CreateDogCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(CreateDogCommand request, CancellationToken cancellationToken)
    {
        var exists = await _context.Dogs.FirstOrDefaultAsync(x => x.Name == request.Name, cancellationToken);

        if (exists is not null)
        {
            throw new DogAlreadyExistsException(exists.Name);
        }
        
        var entity = new Dog(request.Name, request.Color, request.TailLength, request.Weight);
        _context.Dogs.Add(entity);
        
        _logger.LogInformation("Dog with name {Name} was created", request.Name);
        await _context.SaveChangesAsync(cancellationToken);
    }
}