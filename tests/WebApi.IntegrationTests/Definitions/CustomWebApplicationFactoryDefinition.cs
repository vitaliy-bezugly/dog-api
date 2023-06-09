namespace WebApi.IntegrationTests.Definitions;

[CollectionDefinition(name: "web-application-factory")]
public class CustomWebApplicationFactoryDefinition : ICollectionFixture<CustomWebApplicationFactory<IAssemblyMarker>>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}