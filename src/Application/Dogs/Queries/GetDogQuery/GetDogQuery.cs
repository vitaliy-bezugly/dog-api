using Domain.Entities;
using MediatR;

namespace Application.Dogs.Queries.GetDogQuery;

public record GetDogQuery(string Name) : IRequest<Dog>;