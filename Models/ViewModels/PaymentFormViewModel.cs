using TutorFlow.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TutorFlow.Models.ViewModels;

public class PaymentFormViewModel
{
    public PaymentDto Payment { get; set; } = new();
    public IEnumerable<SelectListItem> Lessons { get; set; } = [];
}

