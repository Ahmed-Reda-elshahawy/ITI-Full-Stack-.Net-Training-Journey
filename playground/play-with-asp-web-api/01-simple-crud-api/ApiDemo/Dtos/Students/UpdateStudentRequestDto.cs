using ApiDemo.Validation;
using System.ComponentModel.DataAnnotations;

namespace ApiDemo.Dtos.Students;

[NotEmptyBody]
public class UpdateStudentRequestDto
{
    [MaxLength(50)]
    public string? Name { get; set; }

    [Range(20, 35)]
    public int? Age { get; set; }

    [Range(1, int.MaxValue)]
    public int? DeptId { get; set; }
}
