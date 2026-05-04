using System.ComponentModel.DataAnnotations;
using TutorFlow.Domain.Common;

namespace TutorFlow.Domain.Entities;

public class Payment : BaseEntity
{
    public int LessonId { get; set; }

    [Range(0, 100000)]
    public decimal Amount { get; set; }

    public bool IsPaid { get; set; }

    public DateTime? PaymentDate { get; set; }

    [StringLength(500)]
    public string? Comment { get; set; }

    public Lesson? Lesson { get; set; }
}

