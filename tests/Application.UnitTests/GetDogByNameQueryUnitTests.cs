using Application.Common.Exceptions;
using Application.Dogs.Queries.GetDogByNameQuery;
using Domain.Entities;
using FluentAssertions;

namespace Application.UnitTests;

public class GetDogByNameQueryUnitTests : TestFixtureBase
{
    private readonly GetDogByNameQueryHandler _sut;
    public GetDogByNameQueryUnitTests() : base()
    {
        _sut = new GetDogByNameQueryHandler(ContextMock.Object);
    }

    [Fact]
    public async Task Handle_SendGetQueryWhenDogIsExists_ShouldReturnDog()
    {
        // Arrange
        var query = new GetDogByNameQuery("Fido");
        SetupDogCollectionMock();

        // Act
        Dog actual = await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(GetDogByName(query.Name));
    }
    
    [Fact]
    public async Task Handle_SendQueryWhenDogDoesNotExist_ShouldThrowDogNotFoundException()
    {
        // Arrange
        var query = new GetDogByNameQuery("Not existed dog");
        SetupDogCollectionMock();

        // Act
        Func<Task> act = async () => await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        await act.Should().ThrowAsync<DogNotFoundException>();
    } 
}