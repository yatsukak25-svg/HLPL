using TutorFlow.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TutorFlow.Models.ViewModels;

public class LessonFormViewModel
{
    public LessonDto Lesson { get; set; } = new();
    public IEnumerable<SelectListItem> Tutors { get; set; } = [];
    public IEnumerable<SelectListItem> Students { get; set; } = [];
    public IEnumerable<SelectListItem> Subjects { get; set; } = [];
}

