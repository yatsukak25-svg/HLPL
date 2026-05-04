using System.ComponentModel.DataAnnotations;
using TutorFlow.Domain.Common;

namespace TutorFlow.Domain.Entities;

public class Subject : BaseEntity
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    public string? Description { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public ICollection<LearningMaterial> LearningMaterials { get; set; } = new List<LearningMaterial>();
}

