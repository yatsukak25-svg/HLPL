using TutorFlow.Application.DTOs;

namespace TutorFlow.Models.ViewModels;

public class HomeIndexViewModel
{
    public DashboardStatsDto? TutorStats { get; set; }
    public DashboardStatsDto? StudentStats { get; set; }
    public IReadOnlyList<LessonDto> UpcomingLessons { get; set; } = [];
    public IReadOnlyList<HomeworkDto> HomeworkItems { get; set; } = [];
}

