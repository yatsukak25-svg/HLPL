using System.ComponentModel.DataAnnotations;
using TutorFlow.Domain.Enums;

namespace TutorFlow.Application.DTOs;

public class LessonDto
{
    public int Id { get; set; }

    [Display(Name = "Репетитор")]
    public int TutorId { get; set; }

    [Display(Name = "Учень")]
    public int StudentId { get; set; }

    [Display(Name = "Предмет")]
    public int SubjectId { get; set; }

    [Display(Name = "Початок")]
    public DateTime StartTime { get; set; }

    [Display(Name = "Завершення")]
    public DateTime EndTime { get; set; }

    [Range(0, 100000)]
    [Display(Name = "Ціна")]
    public decimal Price { get; set; }

    [Display(Name = "Статус")]
    public LessonStatus Status { get; set; }

    [Display(Name = "Нотатки")]
    public string? Notes { get; set; }

    public string TutorName { get; set; } = string.Empty;

    public string StudentName { get; set; } = string.Empty;

    public string SubjectName { get; set; } = string.Empty;

    public bool IsHoliday { get; set; }

    public string? HolidayName { get; set; }
}
