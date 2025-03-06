using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Models;

public class Department
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Remote("CheckNameExistance", "Department", AdditionalFields = "Id", ErrorMessage = "Invalid Name!")]
    public string Name { get; set; }

    [Required]
    [Range(5, 100)]
    public int Capacity { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Student> Students { get; set; } = new HashSet<Student>();

    public override string ToString()
    {
        return $"Department {{Id = {Id}, Name = {Name}, Capacity = {Capacity}}}";
    }
}
