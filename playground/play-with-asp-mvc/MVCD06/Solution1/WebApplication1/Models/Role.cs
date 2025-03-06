using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Role
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string Name { get; set; }

    [InverseProperty("Roles")]
    public ICollection<User> Users { get; set; } = new List<User>();
}
