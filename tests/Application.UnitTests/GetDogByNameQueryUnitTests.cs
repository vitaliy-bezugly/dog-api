using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Dogs.Queries.GetDogByNameQuery;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Moq.EntityFrameworkCore;

namespace Application.UnitTests;

public class GetDogByNameQueryUnitTests
{
    private readonly GetDogByNameQueryHandler _sut;
    private readonly Mock<IApplicationDbContext> _contextMock = new();
    public GetDogByNameQueryUnitTests()
    {
        _sut = new GetDogByNameQueryHandler(_contextMock.Object);
    }

    [Fact]
    public async Task Handle_SendGetQueryWhenDogIsExists_ShouldReturnDog()
    {
        // Arrange
        var dog = new Dog("Fido", "white & black", 1, 1);
        var query = new GetDogByNameQuery(dog.Name);
        _contextMock.Setup(x => x.Dogs)
            .ReturnsDbSet(new []{ dog });

        // Act
        Dog actual = await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(dog);
    }
    
    [Fact]
    public async Task Handle_SendQueryWhenDogDoesNotExist_ShouldThrowDogNotFoundException()
    {
        // Arrange
        var query = new GetDogByNameQuery("Fido");
        _contextMock.Setup(x => x.Dogs)
            .ReturnsDbSet(new Dog[0]);

        // Act
        Func<Task> act = async () => await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        await act.Should().ThrowAsync<DogNotFoundException>();
    } 
}