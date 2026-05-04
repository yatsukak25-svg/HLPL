using System.ComponentModel.DataAnnotations;

namespace TutorFlow.Application.DTOs;

public class SubjectDto
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Назва")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Опис")]
    public string? Description { get; set; }
}
