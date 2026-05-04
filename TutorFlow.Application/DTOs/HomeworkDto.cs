using System.ComponentModel.DataAnnotations;
using TutorFlow.Domain.Enums;

namespace TutorFlow.Application.DTOs;

public class HomeworkDto
{
    public int Id { get; set; }

    [Display(Name = "Заняття")]
    public int LessonId { get; set; }

    [Required]
    [Display(Name = "Назва")]
    public string Title { get; set; } = string.Empty;

    [Display(Name = "Опис")]
    public string? Description { get; set; }

    [Display(Name = "Дедлайн")]
    public DateTime Deadline { get; set; }

    [Display(Name = "Статус")]
    public HomeworkStatus Status { get; set; }

    [Display(Name = "Посилання на файл")]
    public string? FileUrl { get; set; }

    public string LessonSummary { get; set; } = string.Empty;
}
