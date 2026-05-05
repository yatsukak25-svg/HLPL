using TutorFlow.Domain.Entities;

namespace TutorFlow.Application.Specifications;

public class LearningMaterialFilterSpecification : BaseSpecification<LearningMaterial>
{
    public LearningMaterialFilterSpecification(string? searchTerm, int? subjectId)
        : base(x =>
            (string.IsNullOrWhiteSpace(searchTerm) || 
                x.Title.ToLower().Contains(searchTerm.ToLower()) || 
                (x.Description != null && x.Description.ToLower().Contains(searchTerm.ToLower()))) &&
            (!subjectId.HasValue || x.SubjectId == subjectId.Value))
    {
        AddInclude(x => x.Subject!);
    }
}
