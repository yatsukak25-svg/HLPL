using TutorFlow.Application.DTOs;
using TutorFlow.Application.Interfaces;
using TutorFlow.Application.Mappings;
using TutorFlow.Domain.Entities;
using TutorFlow.Domain.Enums;

namespace TutorFlow.Application.Services;

public class HomeworkService : IHomeworkService
{
    private readonly IRepository<Homework> _homeworkRepository;

    public HomeworkService(IRepository<Homework> homeworkRepository)
    {
        _homeworkRepository = homeworkRepository;
    }

    public async Task<IReadOnlyList<HomeworkDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var homeworks = await _homeworkRepository.ListAsync(cancellationToken);
        return homeworks.Select(x => x.ToDto()).OrderBy(x => x.Deadline).ToList();
    }

    public async Task<HomeworkDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var homework = await _homeworkRepository.GetByIdAsync(id, cancellationToken);
        return homework?.ToDto();
    }

    public async Task CreateAsync(HomeworkDto dto, CancellationToken cancellationToken = default)
    {
        await _homeworkRepository.AddAsync(new Homework
        {
            LessonId = dto.LessonId,
            Title = dto.Title,
            Description = dto.Description,
            Deadline = dto.Deadline,
            Status = dto.Status,
            FileUrl = dto.FileUrl
        }, cancellationToken);
    }

    public async Task UpdateAsync(HomeworkDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _homeworkRepository.GetByIdAsync(dto.Id, cancellationToken)
            ?? throw new InvalidOperationException("Домашнє завдання не знайдено.");

        entity.LessonId = dto.LessonId;
        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.Deadline = dto.Deadline;
        entity.Status = dto.Status;
        entity.FileUrl = dto.FileUrl;

        await _homeworkRepository.UpdateAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _homeworkRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new InvalidOperationException("Домашнє завдання не знайдено.");

        await _homeworkRepository.DeleteAsync(entity, cancellationToken);
    }

    public async Task MarkAsSubmittedAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _homeworkRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new InvalidOperationException("Домашнє завдання не знайдено.");

        entity.Status = HomeworkStatus.Submitted;
        await _homeworkRepository.UpdateAsync(entity, cancellationToken);
    }
}
