using TutorFlow.Application.Interfaces;
using TutorFlow.Infrastructure;
using TutorFlow.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TutorFlow.Controllers;

[Authorize(Roles = "Student,Tutor,Admin")]
public class MaterialsController : Controller
{
    private const string SelectedMaterialsSessionKey = "selected_materials";
    private readonly ILearningMaterialService _materialService;

    public MaterialsController(ILearningMaterialService materialService)
    {
        _materialService = materialService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var selected = HttpContext.Session.GetJson<List<int>>(SelectedMaterialsSessionKey) ?? [];
        return View(new MaterialsIndexViewModel
        {
            Materials = await _materialService.GetAllAsync(cancellationToken),
            SelectedMaterialIds = selected
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ToggleSelected(int id)
    {
        var selected = HttpContext.Session.GetJson<List<int>>(SelectedMaterialsSessionKey) ?? [];
        if (selected.Contains(id))
        {
            selected.Remove(id);
        }
        else
        {
            selected.Add(id);
        }

        HttpContext.Session.SetJson(SelectedMaterialsSessionKey, selected);
        return RedirectToAction(nameof(Index));
    }
}

