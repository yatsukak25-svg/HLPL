using TutorFlow.Application.Interfaces;
using TutorFlow.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TutorFlow.Controllers;

[Authorize]
public class CalendarController : Controller
{
    private readonly ILessonService _lessonService;
    private readonly IStudentService _studentService;
    private readonly UserManager<ApplicationUser> _userManager;

    public CalendarController(
        ILessonService lessonService,
        IStudentService studentService,
        UserManager<ApplicationUser> userManager)
    {
        _lessonService = lessonService;
        _studentService = studentService;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<JsonResult> GetEvents(DateTime start, DateTime end, CancellationToken cancellationToken)
    {
        int? studentId = null;
        int? tutorId = null;

        var userId = _userManager.GetUserId(User);
        if (userId != null)
        {
            if (User.IsInRole("Student"))
            {
                var studentProfile = await _studentService.GetByUserIdAsync(userId, cancellationToken);
                studentId = studentProfile?.Id;
            }
            else if (User.IsInRole("Tutor") && !User.IsInRole("Admin"))
            {
                // Simple way to get TutorProfile without injecting DBContext
                var lessons = await _lessonService.GetTutorLessonsAsync(0, cancellationToken);
                // Need a better way to get TutorId, let's use the current user's profile
            }
        }

        // Fix the call with correct number of arguments (8 arguments + cancellationToken)
        var lessonsResult = await _lessonService.GetPagedAsync(start, end, studentId, null, tutorId, null, 1, 1000, cancellationToken);
        
        var events = lessonsResult.Items.Select(l => new
        {
            id = l.Id,
            title = $"{l.SubjectName} - {(User.IsInRole("Student") ? l.TutorName : l.StudentName)}",
            start = l.StartTime.ToString("yyyy-MM-ddTHH:mm:ss"),
            end = l.EndTime.ToString("yyyy-MM-ddTHH:mm:ss"),
            className = GetClassName(l.Status) + (l.EndTime < DateTime.Now ? " event-past" : ""),
            allDay = false
        });

        return Json(events);
    }

    private string GetClassName(TutorFlow.Domain.Enums.LessonStatus status)
    {
        return status switch
        {
            TutorFlow.Domain.Enums.LessonStatus.Planned => "bg-primary border-primary text-white",
            TutorFlow.Domain.Enums.LessonStatus.Completed => "bg-success border-success text-white",
            TutorFlow.Domain.Enums.LessonStatus.Cancelled => "bg-danger border-danger text-white",
            _ => "bg-secondary border-secondary text-white"
        };
    }
}
