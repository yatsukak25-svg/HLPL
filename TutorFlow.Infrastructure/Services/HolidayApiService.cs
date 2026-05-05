using System.Net.Http.Json;
using TutorFlow.Application.Interfaces;
using System.Collections.Concurrent;

namespace TutorFlow.Infrastructure.Services;

public class HolidayApiService : IHolidayApiService
{
    private readonly HttpClient _httpClient;
    private static readonly ConcurrentDictionary<int, List<HolidayResponse>> _cache = new();

    public HolidayApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(bool IsHoliday, string? Name)> GetHolidayAsync(DateTime date, CancellationToken cancellationToken = default)
    {
        try
        {
            if (!_cache.TryGetValue(date.Year, out var holidays))
            {
                holidays = await _httpClient.GetFromJsonAsync<List<HolidayResponse>>(
                    $"{date.Year}/UA",
                    cancellationToken);

                if (holidays != null)
                {
                    _cache[date.Year] = holidays;
                }
            }

            var holiday = holidays?.FirstOrDefault(x => DateOnly.Parse(x.Date) == DateOnly.FromDateTime(date));
            return holiday is null ? (false, null) : (true, holiday.LocalName);
        }
        catch
        {
            return (false, null);
        }
    }

    private sealed class HolidayResponse
    {
        public string Date { get; set; } = string.Empty;
        public string LocalName { get; set; } = string.Empty;
    }
}

