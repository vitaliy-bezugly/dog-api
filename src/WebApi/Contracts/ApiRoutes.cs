namespace WebApi.Contracts;

public static class ApiRoutes
{
    public static class Ping
    {
        public const string HealthCheck = "ping";
    }
    public static class Dogs
    {
        private const string Base = "dogs";
        
        public const string GetByName = Base + "/{name}";
        public const string GetAll = Base;
        public const string Create = Base;
    }
}