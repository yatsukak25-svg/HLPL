using TutorFlow.Application.DTOs;

namespace TutorFlow.Models.ViewModels;

public class AdminIndexViewModel
{
    public IReadOnlyList<string> Users { get; set; } = [];
    public IReadOnlyList<LessonDto> Lessons { get; set; } = [];
    public IReadOnlyList<PaymentDto> Payments { get; set; } = [];
}

