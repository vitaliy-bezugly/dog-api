namespace WebApi;

public static class ServiceConfigurator
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "WebApi", Version = "v1" });
        });

        services.AddHealthChecks();
        
        return services;
    }
}