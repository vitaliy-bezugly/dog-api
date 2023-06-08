using System.Reflection;
using AspNetCoreRateLimit;
using WebApi.Filters;

namespace WebApi;

public static class ServiceConfigurator
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddControllers(options =>
        {
            options.Filters.Add<ApiExceptionFilterAttribute>();
        });
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "WebApi", Version = "v1" });
        });
        
        // Used to store rate limit counters and ip rules
        services.AddDistributedMemoryCache();
        services.AddMemoryCache();

        // Load in general configuration from appsettings.json
        services.Configure<IpRateLimitOptions>(options => configuration.GetSection("IpRateLimitingSettings").Bind(options));

        // Inject Counter and Store Rules
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();
        
        // Inject Counter and Store Rules using Distributed Cache Store
        services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>();
        services.AddDistributedRateLimiting();
        
        return services;
    }
}