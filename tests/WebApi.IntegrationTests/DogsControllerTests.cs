using System.Net;
using Application.Common.Models;
using FluentAssertions;
using Newtonsoft.Json;
using WebApi.Contracts;
using WebApi.Contracts.Models;

namespace WebApi.IntegrationTests;

[Collection(name: "web-application-factory")]
public class DogsControllerTests : TestFixtureBase
{
    public DogsControllerTests(CustomWebApplicationFactory<IAssemblyMarker> factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task GetDogs_ReturnsStatusCode200WithListOfDogs()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/" + ApiRoutes.Dogs.GetAll);
        
        // Act
        var response = await Client.SendAsync(request);
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var paginatedList = JsonConvert.DeserializeObject<PaginatedList<DogViewModel>>(responseString);
        paginatedList.Should().NotBeNull();
    }
    
    [Fact]
    public async Task GetDogs_WithPaginatedList_ValidQuery()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/" + ApiRoutes.Dogs.GetAll + "?pageNumber=1&pageSize=10");
        
        // Act
        var response = await Client.SendAsync(request);
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var paginatedList = JsonConvert.DeserializeObject<PaginatedList<DogViewModel>>(responseString);
        paginatedList.Should().NotBeNull();
    }
    
    [Fact]
    public async Task GetDogs_WithPaginatedList_InvalidPageNumber()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/" + ApiRoutes.Dogs.GetAll + "?pageNumber=-1&pageSize=10");
        
        // Act
        var response = await Client.SendAsync(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task GetDogs_WithPaginatedList_InvalidPageSize()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/" + ApiRoutes.Dogs.GetAll + "?pageNumber=1&pageSize=-10");
        
        // Act
        var response = await Client.SendAsync(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task GetDogs_SortingAttributes_ReturnsStatusCode200WithListOfDogs()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/" + ApiRoutes.Dogs.GetAll + "?attribute=name&order=desc");
        
        // Act
        var response = await Client.SendAsync(request);
        
        // Assert
        response.EnsureSuccessStatusCode();
        var responseString = await response.Content.ReadAsStringAsync();
        var paginatedList = JsonConvert.DeserializeObject<PaginatedList<DogViewModel>>(responseString);
        paginatedList.Should().NotBeNull();
    }
    
    [Fact]
    public async Task GetDogs_InvalidSortingAttribute_ReturnsStatusCode400BadRequest()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/" + ApiRoutes.Dogs.GetAll + "?attribute=notexisted&order=desc");
        
        // Act
        var response = await Client.SendAsync(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task GetDogs_InvalidSortingOrder_ReturnsStatusCode400BadRequest()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/" + ApiRoutes.Dogs.GetAll + "?attribute=name&order=invalid");
        
        // Act
        var response = await Client.SendAsync(request);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}