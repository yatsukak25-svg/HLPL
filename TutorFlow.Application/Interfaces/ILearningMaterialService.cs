using TutorFlow.Application.DTOs;

namespace TutorFlow.Application.Interfaces;

public interface ILearningMaterialService
{
    Task<IReadOnlyList<LearningMaterialDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<LearningMaterialDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}

