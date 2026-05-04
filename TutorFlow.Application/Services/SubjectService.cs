using TutorFlow.Application.DTOs;
using TutorFlow.Application.Interfaces;
using TutorFlow.Application.Mappings;
using TutorFlow.Domain.Entities;

namespace TutorFlow.Application.Services;

public class SubjectService : ISubjectService
{
    private readonly IRepository<Subject> _subjectRepository;

    public SubjectService(IRepository<Subject> subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task<IReadOnlyList<SubjectDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var subjects = await _subjectRepository.ListAsync(cancellationToken);
        return subjects.Select(x => x.ToDto()).OrderBy(x => x.Name).ToList();
    }

    public async Task<SubjectDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var subject = await _subjectRepository.GetByIdAsync(id, cancellationToken);
        return subject?.ToDto();
    }

    public async Task CreateAsync(SubjectDto dto, CancellationToken cancellationToken = default)
    {
        await _subjectRepository.AddAsync(new Subject
        {
            Name = dto.Name,
            Description = dto.Description
        }, cancellationToken);
    }

    public async Task UpdateAsync(SubjectDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _subjectRepository.GetByIdAsync(dto.Id, cancellationToken)
            ?? throw new InvalidOperationException("РџСЂРµРґРјРµС‚ РЅРµ Р·РЅР°Р№РґРµРЅРѕ.");

        entity.Name = dto.Name;
        entity.Description = dto.Description;

        await _subjectRepository.UpdateAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _subjectRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new InvalidOperationException("РџСЂРµРґРјРµС‚ РЅРµ Р·РЅР°Р№РґРµРЅРѕ.");

        await _subjectRepository.DeleteAsync(entity, cancellationToken);
    }
}

