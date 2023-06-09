using Application.Common.Interfaces;
using Domain.Entities;
using MockQueryable.Moq;
using Moq;

namespace Application.UnitTests;

public abstract class BaseTestFixture
{
    protected readonly Dog[] Dogs;
    protected readonly Mock<IApplicationDbContext> ContextMock = new();

    protected BaseTestFixture()
    {
        Dogs = new[]
        {
            new Dog("Fido", "white & black", 13, 26),
            new Dog("Buddy", "green & yellow", 18, 31),
            new Dog("Max", "amber & red", 14, 14),
        };
    }
    
    protected void SetupDogCollectionMock()
    {
        var dogMock = Dogs.BuildMock().BuildMockDbSet();
        ContextMock.Setup(x => x.Dogs).Returns(dogMock.Object);
    }
    
    protected Dog GetDogByName(string name)
    {
        return Dogs.First(x => x.Name == name);
    }
}