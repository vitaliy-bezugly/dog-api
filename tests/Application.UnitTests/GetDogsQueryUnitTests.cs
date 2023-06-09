using Application.Common.Models;
using Application.Dogs.Queries.GetDogsQuery;
using Domain.Entities;
using FluentAssertions;

namespace Application.UnitTests;

public class GetDogsQueryUnitTests : BaseTestFixture
{
    private readonly GetDogsQueryHandler _sut;
    
    public GetDogsQueryUnitTests() : base()
    {
        _sut = new GetDogsQueryHandler(ContextMock.Object);
    }

    [Fact]
    public async Task Handle_GetDogsQueryWithPagination_ShouldReturnDogs()
    {
        // Arrange
        SetupDogCollectionMock();
        
        var query = new GetDogsQuery(1, 50, string.Empty, string.Empty);

        // Act
        PaginatedList<Dog> actual = await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        actual.Should().NotBeNull();
        actual.Items.Should().BeEquivalentTo(Dogs);
    }
    
    [Fact]
    public async Task Handle_GetDogsQueryWithPaginationPageSize2_ShouldReturnFirstPage()
    {
        // Arrange
        SetupDogCollectionMock();
        
        var query = new GetDogsQuery(1, 2, string.Empty, string.Empty);

        // Act
        PaginatedList<Dog> actual = await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        actual.Should().NotBeNull();
        actual.Items.Should().BeEquivalentTo(Dogs.Take(2));
    }
    
    [Fact]
    public async Task Handle_GetDogsQueryWithPaginationPageNumber2_ShouldReturnEmptyPage()
    {
        // Arrange
        SetupDogCollectionMock();
        
        var query = new GetDogsQuery(2, 50, string.Empty, string.Empty);

        // Act
        PaginatedList<Dog> actual = await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        actual.Should().NotBeNull();
        actual.Items.Should().BeEquivalentTo(Array.Empty<Dog>());
    }

    [Fact]
    public async Task Handle_GetDogsQueryWithAscSortByName_ShouldReturnAscSortedDogs()
    {
        // Arrange
        SetupDogCollectionMock();
        
        var query = new GetDogsQuery(1, 50, "name", "asc");
        
        // Act
        PaginatedList<Dog> actual = await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        actual.Should().NotBeNull();
        actual.Items.Should().ContainInOrder(Dogs.OrderBy(x => x.Name));
        actual.Items.Should().NotContainInOrder(Dogs.OrderByDescending(x => x.Name));   
    }

    [Fact]
    public async Task Handle_GetDogsQueryWithDescSortByName_ShouldReturnDescSortedGogs()
    {
        // Arrange
        SetupDogCollectionMock();
        
        var query = new GetDogsQuery(1, 50, "name", "desc");
        
        // Act
        PaginatedList<Dog> actual = await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        actual.Should().NotBeNull();
        actual.Items.Should().ContainInOrder(Dogs.OrderByDescending(x => x.Name));
        actual.Items.Should().NotContainInOrder(Dogs.OrderBy(x => x.Name));
    }

    [Fact]
    public async Task Handle_GetDogsQueryWithAscSortByColor_ShouldReturnAscSortedDogs()
    {
        // Arrange
        SetupDogCollectionMock();
        
        var query = new GetDogsQuery(1, 50, "color", "asc");
        
        // Act
        PaginatedList<Dog> actual = await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        actual.Should().NotBeNull();
        actual.Items.Should().ContainInOrder(Dogs.OrderBy(x => x.Color));
        actual.Items.Should().NotContainInOrder(Dogs.OrderByDescending(x => x.Color));
    }
    
    [Fact]
    public async Task Handle_GetDogsQueryWithDescSortByColor_ShouldReturnDescSortedDogs()
    {
        // Arrange
        SetupDogCollectionMock();
        
        var query = new GetDogsQuery(1, 50, "color", "desc");
        
        // Act
        PaginatedList<Dog> actual = await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        actual.Should().NotBeNull();
        actual.Items.Should().ContainInOrder(Dogs.OrderByDescending(x => x.Color));
        actual.Items.Should().NotContainInOrder(Dogs.OrderBy(x => x.Color));
    }
    
    [Fact]
    public async Task Handle_GetDogsQueryWithAscSortByTailLength_ShouldReturnAscSortedDogs()
    {
        // Arrange
        SetupDogCollectionMock();
        
        var query = new GetDogsQuery(1, 50, "tail_length", "asc");
        
        // Act
        PaginatedList<Dog> actual = await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        actual.Should().NotBeNull();
        actual.Items.Should().ContainInOrder(Dogs.OrderBy(x => x.TailLength));
        actual.Items.Should().NotContainInOrder(Dogs.OrderByDescending(x => x.TailLength));
    }
    
    [Fact]
    public async Task Handle_GetDogsQueryWithDescSortByTailLength_ShouldReturnDescSortedDogs()
    {
        // Arrange
        SetupDogCollectionMock();
        
        var query = new GetDogsQuery(1, 50, "tail_length", "desc");
        
        // Act
        PaginatedList<Dog> actual = await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        actual.Should().NotBeNull();
        actual.Items.Should().ContainInOrder(Dogs.OrderByDescending(x => x.TailLength));
        actual.Items.Should().NotContainInOrder(Dogs.OrderBy(x => x.TailLength));
    }
    
    [Fact]
    public async Task Handle_GetDogsQueryWithAscSortByWeight_ShouldReturnAscSortedDogs()
    {
        // Arrange
        SetupDogCollectionMock();
        
        var query = new GetDogsQuery(1, 50, "weight", "asc");
        
        // Act
        PaginatedList<Dog> actual = await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        actual.Should().NotBeNull();
        actual.Items.Should().ContainInOrder(Dogs.OrderBy(x => x.Weight));
        actual.Items.Should().NotContainInOrder(Dogs.OrderByDescending(x => x.Weight));
    }
    
    [Fact]
    public async Task Handle_GetDogsQueryWithDescSortByWeight_ShouldReturnDescSortedDogs()
    {
        // Arrange
        SetupDogCollectionMock();
        
        var query = new GetDogsQuery(1, 50, "weight", "desc");
        
        // Act
        PaginatedList<Dog> actual = await _sut.Handle(query, CancellationToken.None);
        
        // Assert
        actual.Should().NotBeNull();
        actual.Items.Should().ContainInOrder(Dogs.OrderByDescending(x => x.Weight));
        actual.Items.Should().NotContainInOrder(Dogs.OrderBy(x => x.Weight));
    }
}