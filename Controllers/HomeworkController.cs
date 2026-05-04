using TutorFlow.Application.DTOs;
using TutorFlow.Application.Interfaces;
using TutorFlow.Domain.Enums;
using TutorFlow.Infrastructure.Data;
using TutorFlow.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TutorFlow.Controllers;

[Authorize]
public class HomeworkController : Controller
{
    private readonly IHomeworkService _homeworkService;
    private readonly ApplicationDbContext _context;

    public HomeworkController(IHomeworkService homeworkService, ApplicationDbContext context)
    {
        _homeworkService = homeworkService;
        _context = context;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        return View(await _homeworkService.GetAllAsync(cancellationToken));
    }

    [Authorize(Roles = "Tutor,Admin")]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        return View(await BuildFormModelAsync(new HomeworkDto
        {
            Deadline = DateTime.Now.AddDays(7),
            Status = HomeworkStatus.Assigned
        }, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = "Tutor,Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HomeworkFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(await BuildFormModelAsync(model.Homework, cancellationToken));
        }

        await _homeworkService.CreateAsync(model.Homework, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Tutor,Admin")]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var homework = await _homeworkService.GetByIdAsync(id, cancellationToken);
        return homework is null ? NotFound() : View(await BuildFormModelAsync(homework, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = "Tutor,Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(HomeworkFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(await BuildFormModelAsync(model.Homework, cancellationToken));
        }

        await _homeworkService.UpdateAsync(model.Homework, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Tutor,Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var homework = await _homeworkService.GetByIdAsync(id, cancellationToken);
        return homework is null ? NotFound() : View(homework);
    }

    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Tutor,Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        await _homeworkService.DeleteAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Roles = "Student")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Submit(int id, CancellationToken cancellationToken)
    {
        await _homeworkService.MarkAsSubmittedAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    private async Task<HomeworkFormViewModel> BuildFormModelAsync(HomeworkDto homework, CancellationToken cancellationToken)
    {
        var lessons = await _context.Lessons
            .OrderByDescending(x => x.StartTime)
            .Select(x => new { x.Id, Label = x.StartTime.ToString("g") + " / " + x.Subject!.Name })
            .ToListAsync(cancellationToken);

        return new HomeworkFormViewModel
        {
            Homework = homework,
            Lessons = lessons.Select(x => new SelectListItem(x.Label, x.Id.ToString()))
        };
    }
}

