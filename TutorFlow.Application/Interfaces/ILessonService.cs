using TutorFlow.Application.DTOs;
using TutorFlow.Domain.Enums;

namespace TutorFlow.Application.Interfaces;

public interface ILessonService
{
    Task<PagedResult<LessonDto>> GetPagedAsync(
        DateTime? fromDate,
        DateTime? toDate,
        int? studentId,
        int? subjectId,
        LessonStatus? status,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<LessonDto>> GetTutorLessonsAsync(int tutorId, CancellationToken cancellationToken = default);
    Task<LessonDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task CreateAsync(LessonDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(LessonDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

