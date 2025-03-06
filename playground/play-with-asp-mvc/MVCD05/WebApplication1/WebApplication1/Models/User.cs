using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(100)]
    public string? Address { get; set; }
}
