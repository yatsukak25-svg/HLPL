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

    [Display(Name = "URL")]
    public string? Url { get; set; }

    [Display(Name = "Файл")]
    public string? FilePath { get; set; }

    public bool IsFavorite { get; set; }

    public string SubjectName { get; set; } = string.Empty;
}
