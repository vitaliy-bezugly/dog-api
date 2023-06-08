using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Dog> Dogs { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}