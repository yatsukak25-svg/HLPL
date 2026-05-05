using TutorFlow.Application.DTOs;
using TutorFlow.Application.Interfaces;
using TutorFlow.Domain.Entities;
using TutorFlow.Domain.Enums;
using TutorFlow.Infrastructure.Data;
using TutorFlow.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TutorFlow.Controllers;

[Authorize]
public class LessonsController : Controller
{
    private readonly ILessonService _lessonService;
    private readonly IStudentService _studentService;
    private readonly ISubjectService _subjectService;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public LessonsController(
        ILessonService lessonService,
        IStudentService studentService,
        ISubjectService subjectService,
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager)
    {
        _lessonService = lessonService;
        _studentService = studentService;
        _subjectService = subjectService;
        _context = context;
        _userManager = userManager;
    }

    [Authorize(Roles = "Tutor,Admin,Student")]
    public async Task<IActionResult> Index(
        DateTime? fromDate,
        DateTime? toDate,
        int? studentId,
        int? subjectId,
        LessonStatus? status,
        int page = 1,
        CancellationToken cancellationToken = default)
    {
        int? tutorId = null;
        if (User.IsInRole("Student"))
        {
            var userId = _userManager.GetUserId(User);
            if (userId != null)
            {
                var studentProfile = await _studentService.GetByUserIdAsync(userId, cancellationToken);
                studentId = studentProfile?.Id;
            }
        }
        else if (User.IsInRole("Tutor") && !User.IsInRole("Admin"))
        {
            var userId = _userManager.GetUserId(User);
            if (userId != null)
            {
                var tutorProfile = await _context.TutorProfiles.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
                tutorId = tutorProfile?.Id;
            }
        }

        var lessons = await _lessonService.GetPagedAsync(fromDate, toDate, studentId, subjectId, tutorId, status, page, 5, cancellationToken);
        var model = new LessonIndexViewModel
        {
            Lessons = lessons,
            FromDate = fromDate,
            ToDate = toDate,
            StudentId = studentId,
            SubjectId = subjectId,
            Status = status,
            Students = (await _studentService.GetAllAsync(cancellationToken)).Select(x => new SelectListItem(x.FullName, x.Id.ToString())),
            Subjects = (await _subjectService.GetAllAsync(cancellationToken)).Select(x => new SelectListItem(x.Name, x.Id.ToString()))
        };

        return View(model);
    }

    [Authorize(Roles = "Tutor,Admin,Student")]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var lesson = await _lessonService.GetByIdAsync(id, cancellationToken);
        if (lesson is null) return NotFound();

        // Security check: Student can only see their own lessons
        if (User.IsInRole("Student"))
        {
            var userId = _userManager.GetUserId(User);
            var studentProfile = await _studentService.GetByUserIdAsync(userId!, cancellationToken);
            if (lesson.StudentId != studentProfile?.Id) return Forbid();
        }

        return PartialView("_LessonDetails", lesson);
    }

    [Authorize(Roles = "Tutor,Admin")]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        return View(await BuildFormModelAsync(new LessonDto
        {
            StartTime = DateTime.Now.AddDays(1),
            EndTime = DateTime.Now.AddDays(1).AddHours(1),
            Status = LessonStatus.Planned
        }, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = "Tutor,Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LessonFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(await BuildFormModelAsync(model.Lesson, cancellationToken));
        }

        await _lessonService.CreateAsync(model.Lesson, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Tutor,Admin")]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var lesson = await _lessonService.GetByIdAsync(id, cancellationToken);
        return lesson is null ? NotFound() : View(await BuildFormModelAsync(lesson, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = "Tutor,Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(LessonFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(await BuildFormModelAsync(model.Lesson, cancellationToken));
        }

        await _lessonService.UpdateAsync(model.Lesson, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Tutor,Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var lesson = await _lessonService.GetByIdAsync(id, cancellationToken);
        return lesson is null ? NotFound() : View(lesson);
    }

    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Tutor,Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        await _lessonService.DeleteAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    private async Task<LessonFormViewModel> BuildFormModelAsync(LessonDto lesson, CancellationToken cancellationToken)
    {
        var tutors = await _context.TutorProfiles.Select(x => new { x.Id, Name = x.User!.FullName }).ToListAsync(cancellationToken);
        return new LessonFormViewModel
        {
            Lesson = lesson,
            Tutors = tutors.Select(x => new SelectListItem(x.Name, x.Id.ToString())),
            Students = (await _studentService.GetAllAsync(cancellationToken)).Select(x => new SelectListItem(x.FullName, x.Id.ToString())),
            Subjects = (await _subjectService.GetAllAsync(cancellationToken)).Select(x => new SelectListItem(x.Name, x.Id.ToString()))
        };
    }
}

