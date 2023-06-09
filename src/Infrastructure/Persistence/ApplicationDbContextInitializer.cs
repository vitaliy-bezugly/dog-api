using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Persistence;

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
            else
            {
                string errorMessage = "Can not execute migration. Database provider not supported. Supported providers: SqlServer";
                _logger.LogError(errorMessage);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error has occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (_context.Dogs.Any() == false)
        {
            _logger.LogInformation("Seeding database...");
            
            _context.Dogs.AddRange(new List<Dog>
            {
                new Dog {Name = "Neo", Color = "red & amber", TailLength = 22, Weight = 32},
                new Dog {Name = "Jessy", Color = "black & white", TailLength = 7, Weight = 14},
            });

            await _context.SaveChangesAsync();
            _logger.LogInformation("Seeding database completed.");
        }
        else
        {
            _logger.LogInformation("Database already seeded.");
        }
    }
}