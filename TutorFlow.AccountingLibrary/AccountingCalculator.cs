namespace TutorFlow.AccountingLibrary;

public static class AccountingCalculator
{
    public static decimal CalculateMonthlyIncome(IEnumerable<decimal> paidAmounts)
    {
        return paidAmounts.Sum();
    }

    public static decimal CalculateStudentDebt(IEnumerable<(decimal Amount, bool IsPaid)> payments)
    {
        return payments.Where(x => !x.IsPaid).Sum(x => x.Amount);
    }

    public static decimal CalculateLessonTotal(decimal price, decimal extraCharge = 0m, decimal discount = 0m)
    {
        return Math.Max(0m, price + extraCharge - discount);
    }
}

