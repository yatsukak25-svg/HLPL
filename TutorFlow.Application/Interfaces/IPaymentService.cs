using TutorFlow.Application.DTOs;

namespace TutorFlow.Application.Interfaces;

public interface IPaymentService
{
    Task<IReadOnlyList<PaymentDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PaymentDto>> GetUnpaidAsync(CancellationToken cancellationToken = default);
    Task<PaymentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task CreateAsync(PaymentDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(PaymentDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<decimal> GetMonthlyIncomeAsync(int year, int month, CancellationToken cancellationToken = default);
    Task<decimal> GetStudentDebtAsync(int studentId, CancellationToken cancellationToken = default);
}

