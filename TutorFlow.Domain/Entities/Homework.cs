using System.ComponentModel.DataAnnotations;
using TutorFlow.Domain.Common;
using TutorFlow.Domain.Enums;

namespace TutorFlow.Domain.Entities;

public class Homework : BaseEntity
{
    public int LessonId { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }

    public DateTime Deadline { get; set; }

    public HomeworkStatus Status { get; set; } = HomeworkStatus.Assigned;

    [StringLength(500)]
    public string? FileUrl { get; set; }

    public Lesson? Lesson { get; set; }
}

