using System.ComponentModel.DataAnnotations;
using TutorFlow.Domain.Common;

namespace TutorFlow.Domain.Entities;

public class TutorProfile : BaseEntity
{
    [Required]
    public string UserId { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Bio { get; set; }

    [Range(0, 60)]
    public int Experience { get; set; }

    [Range(0, 100000)]
    public decimal HourlyRate { get; set; }

    public ApplicationUser? User { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}

