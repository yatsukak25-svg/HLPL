using TutorFlow.Application.DTOs;

namespace TutorFlow.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardStatsDto> GetTutorDashboardAsync(int tutorId, CancellationToken cancellationToken = default);
    Task<DashboardStatsDto> GetStudentDashboardAsync(int studentId, CancellationToken cancellationToken = default);
}

