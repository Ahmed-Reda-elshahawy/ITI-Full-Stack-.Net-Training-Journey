using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace _00_play_with_auto_mappers.Models;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [Range(0D, Double.MaxValue)]
    public double Price { get; set; }

    [Required]
    [ForeignKey("Category")]
    public int CategoryId { get; set; }

    [Required]
    [Timestamp]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    [Timestamp]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; }
}
