using System.ComponentModel.DataAnnotations;

namespace TutorFlow.Application.DTOs;

public class PaymentDto
{
    public int Id { get; set; }

    [Display(Name = "Заняття")]
    public int LessonId { get; set; }

    [Range(0, 100000)]
    [Display(Name = "Сума")]
    public decimal Amount { get; set; }

    [Display(Name = "Сплачено")]
    public bool IsPaid { get; set; }

    [Display(Name = "Дата оплати")]
    public DateTime? PaymentDate { get; set; }

    [Display(Name = "Коментар")]
    public string? Comment { get; set; }

    public string LessonSummary { get; set; } = string.Empty;

    public string StudentName { get; set; } = string.Empty;
}
