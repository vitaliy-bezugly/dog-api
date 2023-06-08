using MediatR;

namespace Application.Dogs.Commands.CreateDogCommand;

public record CreateDogCommand (string Name, string Color, int TailLength, int Weight) : IRequest;