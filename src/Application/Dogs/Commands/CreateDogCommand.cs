using System.Windows.Input;
using MediatR;

namespace Application.Dogs.Commands;

public record CreateDogCommand (string Name, string Color, int TailLength, int Weight) : IRequest;