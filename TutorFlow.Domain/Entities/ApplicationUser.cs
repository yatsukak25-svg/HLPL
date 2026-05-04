using System.ComponentModel.DataAnnotations;
using TutorFlow.Domain.Validation;
using Microsoft.AspNetCore.Identity;

namespace TutorFlow.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    [Required]
    [StringLength(150)]
    public string FullName { get; set; } = string.Empty;

    [UkrainianPhone]
    public override string? PhoneNumber { get; set; }

    public TutorProfile? TutorProfile { get; set; }

    public StudentProfile? StudentProfile { get; set; }
}

