using System.ComponentModel.DataAnnotations;

namespace TutorFlow.Application.DTOs;

public class StudentDto
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    [Required]
    [Display(Name = "ПІБ")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Телефон")]
    public string? PhoneNumber { get; set; }

    [Range(1, 12)]
    [Display(Name = "Клас")]
    public int Grade { get; set; }

    [Display(Name = "Нотатки")]
    public string? Notes { get; set; }
}
