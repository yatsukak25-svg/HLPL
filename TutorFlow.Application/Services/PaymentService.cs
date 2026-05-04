using TutorFlow.Application.DTOs;
using TutorFlow.Application.Interfaces;
using TutorFlow.Application.Mappings;
using TutorFlow.Application.Specifications;
using TutorFlow.AccountingLibrary;
using TutorFlow.Domain.Entities;

namespace TutorFlow.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IRepository<Payment> _paymentRepository;

    public PaymentService(IRepository<Payment> paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<IReadOnlyList<PaymentDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var payments = await _paymentRepository.ListAsync(cancellationToken);
        return payments.Select(x => x.ToDto()).OrderByDescending(x => x.PaymentDate).ToList();
    }

    public async Task<IReadOnlyList<PaymentDto>> GetUnpaidAsync(CancellationToken cancellationToken = default)
    {
        var payments = await _paymentRepository.ListAsync(new UnpaidLessonsSpecification(), cancellationToken);
        return payments.Select(x => x.ToDto()).ToList();
    }

    public async Task<PaymentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var payment = await _paymentRepository.GetByIdAsync(id, cancellationToken);
        return payment?.ToDto();
    }

    public async Task CreateAsync(PaymentDto dto, CancellationToken cancellationToken = default)
    {
        await _paymentRepository.AddAsync(new Payment
        {
            LessonId = dto.LessonId,
            Amount = dto.Amount,
            IsPaid = dto.IsPaid,
            PaymentDate = dto.PaymentDate,
            Comment = dto.Comment
        }, cancellationToken);
    }

    public async Task UpdateAsync(PaymentDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _paymentRepository.GetByIdAsync(dto.Id, cancellationToken)
            ?? throw new InvalidOperationException("РџР»Р°С‚С–Р¶ РЅРµ Р·РЅР°Р№РґРµРЅРѕ.");

        entity.LessonId = dto.LessonId;
        entity.Amount = dto.Amount;
        entity.IsPaid = dto.IsPaid;
        entity.PaymentDate = dto.PaymentDate;
        entity.Comment = dto.Comment;

        await _paymentRepository.UpdateAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _paymentRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new InvalidOperationException("РџР»Р°С‚С–Р¶ РЅРµ Р·РЅР°Р№РґРµРЅРѕ.");

        await _paymentRepository.DeleteAsync(entity, cancellationToken);
    }

    public async Task<decimal> GetMonthlyIncomeAsync(int year, int month, CancellationToken cancellationToken = default)
    {
        var payments = await _paymentRepository.ListAsync(cancellationToken);
        var paidAmounts = payments
            .Where(x => x.IsPaid && x.PaymentDate.HasValue && x.PaymentDate.Value.Year == year && x.PaymentDate.Value.Month == month)
            .Select(x => x.Amount);

        return AccountingCalculator.CalculateMonthlyIncome(paidAmounts);
    }

    public async Task<decimal> GetStudentDebtAsync(int studentId, CancellationToken cancellationToken = default)
    {
        var payments = await _paymentRepository.ListAsync(cancellationToken);
        var debtItems = payments
            .Where(x => x.Lesson?.StudentId == studentId)
            .Select(x => (x.Amount, x.IsPaid));

        return AccountingCalculator.CalculateStudentDebt(debtItems);
    }
}

