using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Dog
{
    [Key, Column("name")]
    public string Name { get; set; } = string.Empty;
    [Column("color")]
    public string Color { get; set; } = string.Empty;
    [Column("tail_length")]
    public int TailLength { get; set; }
    [Column("weight")]
    public int Weight { get; set; }
}