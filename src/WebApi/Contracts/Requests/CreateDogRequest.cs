using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts.Requests;

public class CreateDogRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Color { get; set; } = string.Empty;
    [Range(1, int.MaxValue)]
    public int TailLength { get; set; }
    [Range(1, int.MaxValue)]
    public int Weight { get; set; }
}