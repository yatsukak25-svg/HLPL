using TutorFlow.Application.DTOs;

namespace TutorFlow.Models.ViewModels;

public class MaterialsIndexViewModel
{
    public IReadOnlyList<LearningMaterialDto> Materials { get; set; } = [];
    public IReadOnlyCollection<int> SelectedMaterialIds { get; set; } = [];
}

