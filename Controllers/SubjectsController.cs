using TutorFlow.Application.DTOs;
using TutorFlow.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TutorFlow.Controllers;

[Authorize(Roles = "Admin,Tutor")]
public class SubjectsController : Controller
{
    private readonly ISubjectService _subjectService;

    public SubjectsController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        return View(await _subjectService.GetAllAsync(cancellationToken));
    }

    public IActionResult Create()
    {
        return View(new SubjectDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SubjectDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        await _subjectService.CreateAsync(dto, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var subject = await _subjectService.GetByIdAsync(id, cancellationToken);
        return subject is null ? NotFound() : View(subject);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(SubjectDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        await _subjectService.UpdateAsync(dto, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var subject = await _subjectService.GetByIdAsync(id, cancellationToken);
        return subject is null ? NotFound() : View(subject);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        await _subjectService.DeleteAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}

