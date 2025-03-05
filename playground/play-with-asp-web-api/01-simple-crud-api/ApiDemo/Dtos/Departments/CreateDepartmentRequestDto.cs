using ApiDemo.Validation;
using System.ComponentModel.DataAnnotations;

namespace ApiDemo.Dtos.Departments;

[NotEmptyBody]
public class CreateDepartmentRequestDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [Range(10, 40)]
    public int Capacity { get; set; }
}
