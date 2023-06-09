using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApi.IntegrationTests;

public abstract class TestFixtureBase
{
    protected readonly HttpClient Client;

    protected TestFixtureBase(CustomWebApplicationFactory<IAssemblyMarker> factory)
    {
        Client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = true
        });
    }
}