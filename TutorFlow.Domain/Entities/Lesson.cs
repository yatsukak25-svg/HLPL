using System.ComponentModel.DataAnnotations;
using TutorFlow.Domain.Common;
using TutorFlow.Domain.Enums;

namespace TutorFlow.Domain.Entities;

public class Lesson : BaseEntity
{
    public int TutorId { get; set; }

    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    [Range(0, 100000)]
    public decimal Price { get; set; }

    public LessonStatus Status { get; set; } = LessonStatus.Planned;

    [StringLength(1000)]
    public string? Notes { get; set; }

    public TutorProfile? Tutor { get; set; }

    public StudentProfile? Student { get; set; }

    public Subject? Subject { get; set; }

    public Homework? Homework { get; set; }

    public Payment? Payment { get; set; }
}

