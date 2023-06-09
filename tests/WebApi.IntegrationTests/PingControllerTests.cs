using System.Net;
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
        await Task.Delay(1000);
        var response = await Client.SendAsync(request);
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        responseString.Should().Be("Dogs house service. Version 1.0.1");
    }

    [Fact]
    public async Task SendToManyRequests_After10RequestShouldGetAlways429StatusCode()
    {
        // Arrange
        int requestsCount = 15;
        
        // Act
        var tasks = Enumerable.Range(0, requestsCount).Select(x =>
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/" + ApiRoutes.Ping.HealthCheck);
            var task = Client.SendAsync(request);
            return task;
        });
        
        var responses = await Task.WhenAll(tasks);
        
        // Assert
        int expectedSuccessResponses = 10, expectedTooManyRequestsResponses = 5;
        responses.Where(x => x.IsSuccessStatusCode).Should().HaveCount(expectedSuccessResponses);
        responses.Where(x => x.StatusCode == HttpStatusCode.TooManyRequests).Should().HaveCount(expectedTooManyRequestsResponses);
    }
}