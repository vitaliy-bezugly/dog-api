using System.Text.Json.Serialization;
using Domain.Entities;
using WebApi.Contracts.Requests;
using WebApi.Mappings;

namespace WebApi.Contracts.Models;

public class DogViewModel : IMapFrom<Dog>, IMapFrom<CreateDogRequest>
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("color")]
    public string Color { get; set; } = string.Empty;
    [JsonPropertyName("tail_length")]
    public int TailLength { get; set; }
    [JsonPropertyName("weight")]
    public int Weight { get; set; }
}