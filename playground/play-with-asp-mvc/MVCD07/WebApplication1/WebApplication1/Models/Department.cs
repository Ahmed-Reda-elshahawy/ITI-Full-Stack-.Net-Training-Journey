using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Department
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [Range(5, 100)]
    public int Capacity { get; set; }

    [InverseProperty("Department")]
    public ICollection<Student> Students { get; set; } = new List<Student>();
}
