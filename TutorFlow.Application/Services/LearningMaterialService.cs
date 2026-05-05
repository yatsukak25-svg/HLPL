using TutorFlow.Application.DTOs;
using TutorFlow.Application.Interfaces;
using TutorFlow.Application.Mappings;
using TutorFlow.Application.Specifications;
using TutorFlow.Domain.Entities;

namespace TutorFlow.Application.Services;

public class LearningMaterialService : ILearningMaterialService
{
    private readonly IRepository<LearningMaterial> _materialRepository;
    private readonly IRepository<UserFavoriteMaterial> _favoriteRepository;
    private readonly IRepository<Lesson> _lessonRepository;

    public LearningMaterialService(
        IRepository<LearningMaterial> materialRepository,
        IRepository<UserFavoriteMaterial> favoriteRepository,
        IRepository<Lesson> lessonRepository)
    {
        _materialRepository = materialRepository;
        _favoriteRepository = favoriteRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<IReadOnlyList<LearningMaterialDto>> GetAllAsync(
        string? userId = null, 
        string? searchTerm = null, 
        int? subjectId = null, 
        CancellationToken cancellationToken = default)
    {
        var spec = new LearningMaterialFilterSpecification(searchTerm, subjectId);
        var materials = await _materialRepository.ListAsync(spec, cancellationToken);
        
        var favoriteIds = userId != null 
            ? await GetUserFavoriteIdsAsync(userId, cancellationToken) 
            : new List<int>();

        return materials.Select(x => 
        {
            var dto = x.ToDto();
            dto.FilePath = x.FilePath;
            dto.IsFavorite = favoriteIds.Contains(x.Id);
            return dto;
        }).OrderBy(x => x.SubjectName).ThenBy(x => x.Title).ToList();
    }

    public async Task<LearningMaterialDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var material = await _materialRepository.GetByIdAsync(id, cancellationToken);
        if (material == null) return null;
        
        var dto = material.ToDto();
        dto.FilePath = material.FilePath;
        return dto;
    }

    public async Task CreateAsync(LearningMaterialDto dto, CancellationToken cancellationToken = default)
    {
        await _materialRepository.AddAsync(new LearningMaterial
        {
            SubjectId = dto.SubjectId,
            Title = dto.Title,
            Description = dto.Description,
            Url = dto.Url ?? string.Empty,
            FilePath = dto.FilePath
        }, cancellationToken);
    }

    public async Task UpdateAsync(LearningMaterialDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _materialRepository.GetByIdAsync(dto.Id, cancellationToken)
            ?? throw new InvalidOperationException("Матеріал не знайдено.");

        entity.SubjectId = dto.SubjectId;
        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.Url = dto.Url ?? string.Empty;
        if (dto.FilePath != null) entity.FilePath = dto.FilePath;

        await _materialRepository.UpdateAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _materialRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new InvalidOperationException("Матеріал не знайдено.");

        await _materialRepository.DeleteAsync(entity, cancellationToken);
    }

    public async Task ToggleFavoriteAsync(string userId, int materialId, CancellationToken cancellationToken = default)
    {
        var favorites = await _favoriteRepository.ListAsync(cancellationToken);
        var existing = favorites.FirstOrDefault(x => x.UserId == userId && x.LearningMaterialId == materialId);

        if (existing != null)
        {
            await _favoriteRepository.DeleteAsync(existing, cancellationToken);
        }
        else
        {
            await _favoriteRepository.AddAsync(new UserFavoriteMaterial
            {
                UserId = userId,
                LearningMaterialId = materialId
            }, cancellationToken);
        }
    }

    public async Task<IReadOnlyList<int>> GetUserFavoriteIdsAsync(string userId, CancellationToken cancellationToken = default)
    {
        var favorites = await _favoriteRepository.ListAsync(cancellationToken);
        return favorites.Where(x => x.UserId == userId).Select(x => x.LearningMaterialId).ToList();
    }
}
