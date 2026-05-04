using TutorFlow.Application.DTOs;

namespace TutorFlow.Application.Interfaces;

public interface ISubjectService
{
    Task<IReadOnlyList<SubjectDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SubjectDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task CreateAsync(SubjectDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(SubjectDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

