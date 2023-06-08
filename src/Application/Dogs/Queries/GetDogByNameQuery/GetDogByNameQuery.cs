using Domain.Entities;
using MediatR;

namespace Application.Dogs.Queries.GetDogByNameQuery;

public record GetDogByNameQuery(string Name) : IRequest<Dog>;