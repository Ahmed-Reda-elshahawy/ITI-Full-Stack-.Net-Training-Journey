using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiDemo.Models;

public class Student
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [Range(20, 35)]
    public int Age { get; set; }

    [Required]
    [ForeignKey("Department")]
    public int DeptId { get; set; }

    [JsonIgnore]
    [InverseProperty("Students")]
    public virtual Department Department { get; set; }
}
