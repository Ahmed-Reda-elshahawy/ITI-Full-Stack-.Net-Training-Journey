using ApiDemo.Validation;
using System.ComponentModel.DataAnnotations;

namespace ApiDemo.Dtos.Departments;

[NotEmptyBody]
public class UpdateDepartmentRequestDto
{
    [MaxLength(50)]
    public string? Name { get; set; }

    [Range(10, 40)]
    public int? Capacity { get; set; }
}
