using Application.Common.Exceptions;
using Application.Dogs.Commands.CreateDogCommand;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.UnitTests;

public class CreateDogCommandUnitTests : TestFixtureBase
{
    private readonly CreateDogCommandHandler _sut;
    
    public CreateDogCommandUnitTests()
    {
        var loggerMock = new Mock<ILogger<CreateDogCommandHandler>>();
        _sut = new CreateDogCommandHandler(ContextMock.Object, loggerMock.Object);
    }
    
    [Fact]
    public async Task Handle_GivenValidRequest_ShouldCreateDog()
    {
        // Arrange
        var request = new CreateDogCommand("Test", "Black", 10, 10);
        SetupDogCollectionMock();

        // Act
        await _sut.Handle(request, CancellationToken.None);
        
        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public async Task Handle_CreateDogWithNotUniqueName_ShouldThrowDogAlreadyExistsException()
    {
        // Arrange
        var request = new CreateDogCommand("Fido", "Black", 10, 10);
        SetupDogCollectionMock();

        // Act
        Func<Task> act = async () => await _sut.Handle(request, CancellationToken.None);
        
        // Assert
        await act.Should().ThrowAsync<DogAlreadyExistsException>();
    }
}