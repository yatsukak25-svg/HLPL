using TutorFlow.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TutorFlow.Models.ViewModels;

public class HomeworkFormViewModel
{
    public HomeworkDto Homework { get; set; } = new();
    public IEnumerable<SelectListItem> Lessons { get; set; } = [];
}

