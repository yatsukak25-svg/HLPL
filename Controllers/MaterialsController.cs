using TutorFlow.Application.DTOs;
using TutorFlow.Application.Interfaces;
using TutorFlow.Domain.Entities;
using TutorFlow.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TutorFlow.Controllers;

[Authorize]
public class MaterialsController : Controller
{
    private readonly ILearningMaterialService _materialService;
    private readonly ISubjectService _subjectService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public MaterialsController(
        ILearningMaterialService materialService,
        ISubjectService subjectService,
        UserManager<ApplicationUser> userManager,
        IWebHostEnvironment webHostEnvironment)
    {
        _materialService = materialService;
        _subjectService = subjectService;
        _userManager = userManager;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index(string? searchTerm, int? subjectId, CancellationToken cancellationToken)
    {
        var userId = _userManager.GetUserId(User);
        var materials = await _materialService.GetAllAsync(userId, searchTerm, subjectId, cancellationToken);
        var subjects = await _subjectService.GetAllAsync(cancellationToken);

        var model = new MaterialsIndexViewModel
        {
            Materials = materials,
            SearchTerm = searchTerm,
            SubjectId = subjectId,
            Subjects = subjects.Select(s => new SelectListItem(s.Name, s.Id.ToString(), s.Id == subjectId)),
            NewMaterial = new LearningMaterialDto()
        };

        return View(model);
    }

    [HttpPost]
    [Authorize(Roles = "Tutor,Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MaterialsIndexViewModel model, IFormFile? file, CancellationToken cancellationToken)
    {
        if (model.NewMaterial == null) return RedirectToAction(nameof(Index));

        if (file != null && file.Length > 0)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "materials");
            Directory.CreateDirectory(uploadsFolder);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream, cancellationToken);
            }

            model.NewMaterial.FilePath = "/uploads/materials/" + fileName;
        }

        await _materialService.CreateAsync(model.NewMaterial, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleFavorite(int id, CancellationToken cancellationToken)
    {
        var userId = _userManager.GetUserId(User);
        if (userId != null)
        {
            await _materialService.ToggleFavoriteAsync(userId, id, cancellationToken);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Authorize(Roles = "Tutor,Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _materialService.DeleteAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}
