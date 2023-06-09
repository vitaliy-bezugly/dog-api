using Application.Common.Interfaces;
using Application.Dogs.Queries.GetDogByNameQuery;
using Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using Moq.EntityFrameworkCore;

namespace Application.UnitTests;

public class GetDogByNameQueryUnitTests
{
    private readonly GetDogByNameQueryHandler _sut;

    public GetDogByNameQueryUnitTests()
    {
        var employeeContextMock = new Mock<IApplicationDbContext>();
        employeeContextMock.Setup<DbSet<Dog>>(x => x.Dogs)
            .ReturnsDbSet(new []{ new Dog( "Fido", "white & black", 1, 1) });
        
        var context = employeeContextMock.Object;
        _sut = new GetDogByNameQueryHandler(context);
    }

    [Fact]
    public async Task Handle_SendGetQueryWhenDogIsExists_ShouldReturnDog()
    {
        // Arrange
        var query = new GetDogByNameQuery("Fido");
        
        // Act
        Dog expected = await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        expected.Should().NotBeNull();
        expected.Should().BeEquivalentTo(new Dog("Fido", "white & black", 1, 1));
    }
}