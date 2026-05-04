using TutorFlow.Application.DTOs;
using TutorFlow.Application.Interfaces;
using TutorFlow.Application.Mappings;
using TutorFlow.Domain.Entities;

namespace TutorFlow.Application.Services;

public class LearningMaterialService : ILearningMaterialService
{
    private readonly IRepository<LearningMaterial> _materialRepository;

    public LearningMaterialService(IRepository<LearningMaterial> materialRepository)
    {
        _materialRepository = materialRepository;
    }

    public async Task<IReadOnlyList<LearningMaterialDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var materials = await _materialRepository.ListAsync(cancellationToken);
        return materials.Select(x => x.ToDto()).OrderBy(x => x.SubjectName).ThenBy(x => x.Title).ToList();
    }

    public async Task<LearningMaterialDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var material = await _materialRepository.GetByIdAsync(id, cancellationToken);
        return material?.ToDto();
    }
}

