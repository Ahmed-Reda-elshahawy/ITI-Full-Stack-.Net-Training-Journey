using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Student
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    [ForeignKey("Department")]
    public int DeptId { get; set; }

    [InverseProperty("Students")]
    public Department Department { get; set; }
}
