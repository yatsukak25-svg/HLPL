using System.ComponentModel.DataAnnotations;

namespace TutorFlow.Application.DTOs;

public class LearningMaterialDto
{
    public int Id { get; set; }

    [Display(Name = "Предмет")]
    public int SubjectId { get; set; }

    [Required]
    [Display(Name = "Назва")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Опис")]
    public string? Description { get; set; }

    [Required]
    [Display(Name = "URL")]
    public string Url { get; set; } = string.Empty;

    public string SubjectName { get; set; } = string.Empty;
}
