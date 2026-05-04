using TutorFlow.Application.DTOs;
using TutorFlow.Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TutorFlow.Models.ViewModels;

public class LessonIndexViewModel
{
    public PagedResult<LessonDto> Lessons { get; set; } = new();
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public int? StudentId { get; set; }
    public int? SubjectId { get; set; }
    public LessonStatus? Status { get; set; }
    public IEnumerable<SelectListItem> Students { get; set; } = [];
    public IEnumerable<SelectListItem> Subjects { get; set; } = [];
}

