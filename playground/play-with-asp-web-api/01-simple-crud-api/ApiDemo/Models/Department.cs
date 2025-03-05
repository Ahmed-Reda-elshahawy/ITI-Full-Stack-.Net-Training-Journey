using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiDemo.Models;

public class Department
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [Range(10, 40)]
    public int Capacity { get; set; }

    [JsonIgnore]
    [InverseProperty("Department")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
