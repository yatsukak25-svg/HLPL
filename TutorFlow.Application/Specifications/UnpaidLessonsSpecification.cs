using TutorFlow.Domain.Entities;

namespace TutorFlow.Application.Specifications;

public class UnpaidLessonsSpecification : BaseSpecification<Payment>
{
    public UnpaidLessonsSpecification()
        : base(payment => !payment.IsPaid)
    {
        AddInclude(x => x.Lesson!);
        ApplyOrderBy(x => x.PaymentDate ?? DateTime.MaxValue);
    }
}

