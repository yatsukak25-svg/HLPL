using TutorFlow.Application.DTOs;
using TutorFlow.Application.Interfaces;
using TutorFlow.Domain.Entities;
using TutorFlow.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TutorFlow.Controllers;

[Authorize(Roles = "Tutor,Admin")]
public class StudentsController : Controller
{
    private readonly IStudentService _studentService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _context;

    public StudentsController(
        IStudentService studentService,
        UserManager<ApplicationUser> userManager,
        ApplicationDbContext context)
    {
        _studentService = studentService;
        _userManager = userManager;
        _context = context;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        if (User.IsInRole("Admin"))
        {
            return View(await _studentService.GetAllAsync(cancellationToken));
        }

        var userId = _userManager.GetUserId(User);
        var tutor = await _context.TutorProfiles.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        
        if (tutor == null) return Forbid();

        var students = await _studentService.GetTutorStudentsAsync(tutor.Id, cancellationToken);
        return View(students);
    }

    [HttpPost]
    [Authorize(Roles = "Tutor,Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddByFullName(string fullName, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            TempData["Error"] = "Будь ласка, введіть ПІБ учня.";
            return RedirectToAction(nameof(Index));
        }

        var foundStudents = await _studentService.GetByFullNameAsync(fullName, cancellationToken);
        var student = foundStudents.FirstOrDefault(); // For simplicity, take the first match

        if (student == null)
        {
            TempData["Error"] = $"Учня з ПІБ '{fullName}' не знайдено в системі.";
            return RedirectToAction(nameof(Index));
        }

        var userId = _userManager.GetUserId(User);
        var tutor = await _context.TutorProfiles.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        
        if (tutor == null) return Forbid();

        await _studentService.LinkToTutorAsync(student.Id, tutor.Id, cancellationToken);
        TempData["Success"] = $"Учень {student.FullName} успішно доданий до вашого списку.";

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Create()
    {
        return View(new StudentDto());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StudentDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        await _studentService.CreateAsync(dto, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetByIdAsync(id, cancellationToken);
        return student is null ? NotFound() : View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(StudentDto dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(dto);
        }

        await _studentService.UpdateAsync(dto, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var student = await _studentService.GetByIdAsync(id, cancellationToken);
        return student is null ? NotFound() : View(student);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        await _studentService.DeleteAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}

