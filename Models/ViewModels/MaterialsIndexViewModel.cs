using TutorFlow.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TutorFlow.Models.ViewModels;

public class MaterialsIndexViewModel
{
    public IReadOnlyList<LearningMaterialDto> Materials { get; set; } = [];
    
    // For filtering
    public string? SearchTerm { get; set; }
    public int? SubjectId { get; set; }
    public IEnumerable<SelectListItem> Subjects { get; set; } = [];

    // For Tutor CRUD (Optional, could be a separate view, but for now we'll put a create form on Index)
    public LearningMaterialDto? NewMaterial { get; set; }
}
