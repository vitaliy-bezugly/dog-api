namespace Application.Common.Exceptions;

public class DogNotFoundException : Exception
{
    public DogNotFoundException(string name) : base($"There is no dog with given: {name} name.")
    { }
}