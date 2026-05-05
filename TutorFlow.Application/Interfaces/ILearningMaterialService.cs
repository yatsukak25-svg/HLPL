using TutorFlow.Application.DTOs;

namespace TutorFlow.Application.Interfaces;

public interface ILearningMaterialService
{
    Task<IReadOnlyList<LearningMaterialDto>> GetAllAsync(string? userId = null, string? searchTerm = null, int? subjectId = null, CancellationToken cancellationToken = default);
    Task<LearningMaterialDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task CreateAsync(LearningMaterialDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(LearningMaterialDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    
    Task ToggleFavoriteAsync(string userId, int materialId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<int>> GetUserFavoriteIdsAsync(string userId, CancellationToken cancellationToken = default);
}
