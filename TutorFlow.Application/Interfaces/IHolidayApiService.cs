namespace TutorFlow.Application.Interfaces;

public interface IHolidayApiService
{
    Task<(bool IsHoliday, string? Name)> GetHolidayAsync(DateTime date, CancellationToken cancellationToken = default);
}

