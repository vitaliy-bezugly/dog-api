using FluentAssertions;
using WebApi.Contracts;

namespace WebApi.IntegrationTests;

[Collection(name: "web-application-factory")]
public class PingControllerTests : TestFixtureBase
{
    public PingControllerTests(CustomWebApplicationFactory<IAssemblyMarker> factory) : base(factory)
    { }
    
    [Fact]
    public async Task SendPing_ReturnsStatusCode200WithMessage()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/" + ApiRoutes.Ping.HealthCheck);
        
        // Act
        var response = await Client.SendAsync(request);
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        responseString.Should().Be("Dogs house service. Version 1.0.1");
    }
}