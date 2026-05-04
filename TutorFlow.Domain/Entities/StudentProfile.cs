using System.ComponentModel.DataAnnotations;
using TutorFlow.Domain.Common;

namespace TutorFlow.Domain.Entities;

public class StudentProfile : BaseEntity
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [Range(1, 12)]
    public int Grade { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }

    public ApplicationUser? User { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}

