using TutorFlow.Application.DTOs;

namespace TutorFlow.Application.Interfaces;

public interface IHomeworkService
{
    Task<IReadOnlyList<HomeworkDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<HomeworkDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task CreateAsync(HomeworkDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(HomeworkDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task MarkAsSubmittedAsync(int id, CancellationToken cancellationToken = default);
}

