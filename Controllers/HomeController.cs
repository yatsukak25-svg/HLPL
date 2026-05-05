using System.Diagnostics;
using TutorFlow.Application.Interfaces;
using TutorFlow.Domain.Entities;
using TutorFlow.Infrastructure.Data;
using TutorFlow.Models;
using TutorFlow.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TutorFlow.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDashboardService _dashboardService;
    private readonly ILessonService _lessonService;
    private readonly IHomeworkService _homeworkService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public HomeController(
        ILogger<HomeController> logger,
        IDashboardService dashboardService,
        ILessonService lessonService,
        IHomeworkService homeworkService,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context)
    {
        _logger = logger;
        _dashboardService = dashboardService;
        _lessonService = lessonService;
        _homeworkService = homeworkService;
        _userManager = userManager;
        _context = context;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var model = new HomeIndexViewModel();

        if (User.Identity?.IsAuthenticated == true)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user is not null)
            {
                if (await _userManager.IsInRoleAsync(user, "Tutor"))
                {
                    var tutor = await _context.TutorProfiles.FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);
                    if (tutor != null)
                    {
                        if (!tutor.IsApproved)
                        {
                            ViewBag.NotApproved = true;
                        }
                        ViewBag.TutorCode = tutor.TutorCode;

                        var tutorId = tutor.Id;
                        model.TutorStats = await _dashboardService.GetTutorDashboardAsync(tutorId, cancellationToken);
                        model.UpcomingLessons = (await _lessonService.GetTutorLessonsAsync(tutorId, cancellationToken))
                            .Where(x => x.StartTime >= DateTime.Now)
                            .Take(5)
                            .ToList();
                    }
                }

                if (await _userManager.IsInRoleAsync(user, "Student"))
                {
                    var studentId = await _context.StudentProfiles.Where(x => x.UserId == user.Id).Select(x => x.Id).FirstAsync(cancellationToken);
                    model.StudentStats = await _dashboardService.GetStudentDashboardAsync(studentId, cancellationToken);
                }
            }
        }

        model.HomeworkItems = (await _homeworkService.GetAllAsync(cancellationToken)).Take(5).ToList();
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

