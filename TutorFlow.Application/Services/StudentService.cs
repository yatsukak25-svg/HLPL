using TutorFlow.Application.DTOs;
using TutorFlow.Application.Interfaces;
using TutorFlow.Application.Mappings;
using TutorFlow.Domain.Entities;

namespace TutorFlow.Application.Services;

public class StudentService : IStudentService
{
    private readonly IRepository<StudentProfile> _studentRepository;
    private readonly IUserManagementService _userManagementService;

    public StudentService(IRepository<StudentProfile> studentRepository, IUserManagementService userManagementService)
    {
        _studentRepository = studentRepository;
        _userManagementService = userManagementService;
    }

    public async Task<IReadOnlyList<StudentDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var students = await _studentRepository.ListAsync(cancellationToken);
        return students.Select(x => x.ToDto()).OrderBy(x => x.FullName).ToList();
    }

    public async Task<StudentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var student = await _studentRepository.GetByIdAsync(id, cancellationToken);
        return student?.ToDto();
    }

    public async Task CreateAsync(StudentDto dto, CancellationToken cancellationToken = default)
    {
        var userId = await _userManagementService.CreateStudentUserAsync(dto, cancellationToken);
        var entity = new StudentProfile
        {
            UserId = userId,
            Grade = dto.Grade,
            Notes = dto.Notes
        };

        await _studentRepository.AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(StudentDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _studentRepository.GetByIdAsync(dto.Id, cancellationToken)
            ?? throw new InvalidOperationException("Учня не знайдено.");

        entity.Grade = dto.Grade;
        entity.Notes = dto.Notes;

        await _userManagementService.UpdateStudentUserAsync(dto, cancellationToken);
        await _studentRepository.UpdateAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _studentRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new InvalidOperationException("Учня не знайдено.");

        await _userManagementService.DeleteUserAsync(entity.UserId, cancellationToken);
        await _studentRepository.DeleteAsync(entity, cancellationToken);
    }
}

