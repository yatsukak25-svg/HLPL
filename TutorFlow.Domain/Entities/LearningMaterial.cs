using System.ComponentModel.DataAnnotations;
using TutorFlow.Domain.Common;

namespace TutorFlow.Domain.Entities;

public class LearningMaterial : BaseEntity
{
    public int SubjectId { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Required]
    [StringLength(500)]
    public string Url { get; set; } = string.Empty;

    public Subject? Subject { get; set; }
}

