namespace Application.Common.Exceptions;

public class DogAlreadyExistsException : Exception
{
    public DogAlreadyExistsException(string dogName) : base($"Dog with name {dogName} already exists.")
    { }
}