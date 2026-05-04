using TutorFlow.Application.Interfaces;
using TutorFlow.Domain.Entities;
using TutorFlow.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TutorFlow.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILessonService _lessonService;
    private readonly IPaymentService _paymentService;

    public AdminController(
        UserManager<ApplicationUser> userManager,
        ILessonService lessonService,
        IPaymentService paymentService)
    {
        _userManager = userManager;
        _lessonService = lessonService;
        _paymentService = paymentService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var users = _userManager.Users.Select(x => $"{x.FullName} ({x.Email})").OrderBy(x => x).ToList();
        var lessons = await _lessonService.GetPagedAsync(null, null, null, null, null, 1, 20, cancellationToken);
        var payments = await _paymentService.GetAllAsync(cancellationToken);

        return View(new AdminIndexViewModel
        {
            Users = users,
            Lessons = lessons.Items,
            Payments = payments
        });
    }
}

