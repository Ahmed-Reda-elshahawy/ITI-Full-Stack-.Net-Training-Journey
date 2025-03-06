using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Models;

public class Student
{
    [Key]
    public int Id { get; set; }

    [Required]
    [NotNull]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    [NotNull]
    [EmailAddress]
    [Remote("CheckEmailExistence", "Student", AdditionalFields = "Id", ErrorMessage = "The email is already registered!")]
    public string Email { get; set; }

    [NotNull]
    [Range(4, 100)]
    public int Age { get; set; }

    [NotNull]
    public int DeptId { get; set; }

    [ForeignKey("DeptId")]
    [InverseProperty("Students")]
    public virtual Department Department { get; set; }
}
