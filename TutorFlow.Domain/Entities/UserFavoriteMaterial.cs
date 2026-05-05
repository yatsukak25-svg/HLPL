using TutorFlow.Domain.Common;

namespace TutorFlow.Domain.Entities;

public class UserFavoriteMaterial : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public int LearningMaterialId { get; set; }

    public ApplicationUser? User { get; set; }
    public LearningMaterial? LearningMaterial { get; set; }
}
