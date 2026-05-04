using TutorFlow.Application.DTOs;
using TutorFlow.Application.Interfaces;
using TutorFlow.Infrastructure.Data;
using TutorFlow.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TutorFlow.Controllers;

[Authorize]
public class PaymentsController : Controller
{
    private readonly IPaymentService _paymentService;
    private readonly ApplicationDbContext _context;

    public PaymentsController(IPaymentService paymentService, ApplicationDbContext context)
    {
        _paymentService = paymentService;
        _context = context;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        ViewBag.MonthIncome = await _paymentService.GetMonthlyIncomeAsync(DateTime.Today.Year, DateTime.Today.Month, cancellationToken);
        ViewBag.Unpaid = await _paymentService.GetUnpaidAsync(cancellationToken);
        return View(await _paymentService.GetAllAsync(cancellationToken));
    }

    [Authorize(Roles = "Tutor,Admin")]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        return View(await BuildFormModelAsync(new PaymentDto(), cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = "Tutor,Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PaymentFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(await BuildFormModelAsync(model.Payment, cancellationToken));
        }

        await _paymentService.CreateAsync(model.Payment, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Tutor,Admin")]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var payment = await _paymentService.GetByIdAsync(id, cancellationToken);
        return payment is null ? NotFound() : View(await BuildFormModelAsync(payment, cancellationToken));
    }

    [HttpPost]
    [Authorize(Roles = "Tutor,Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(PaymentFormViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(await BuildFormModelAsync(model.Payment, cancellationToken));
        }

        await _paymentService.UpdateAsync(model.Payment, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Tutor,Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var payment = await _paymentService.GetByIdAsync(id, cancellationToken);
        return payment is null ? NotFound() : View(payment);
    }

    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "Tutor,Admin")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        await _paymentService.DeleteAsync(id, cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    private async Task<PaymentFormViewModel> BuildFormModelAsync(PaymentDto payment, CancellationToken cancellationToken)
    {
        var lessons = await _context.Lessons
            .OrderByDescending(x => x.StartTime)
            .Select(x => new { x.Id, Label = x.StartTime.ToString("g") + " / " + x.Student!.User!.FullName })
            .ToListAsync(cancellationToken);

        return new PaymentFormViewModel
        {
            Payment = payment,
            Lessons = lessons.Select(x => new SelectListItem(x.Label, x.Id.ToString()))
        };
    }
}

