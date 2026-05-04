namespace TutorFlow.Application.DTOs;

public class DashboardStatsDto
{
    public int TotalStudents { get; set; }

    public int PlannedLessons { get; set; }

    public int PendingHomework { get; set; }

    public decimal MonthlyIncome { get; set; }

    public decimal OutstandingDebt { get; set; }
}

