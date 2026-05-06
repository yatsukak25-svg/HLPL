using TutorFlow.Application.DTOs;
using TutorFlow.Application.Interfaces;
using TutorFlow.Application.Mappings;
using TutorFlow.Domain.Entities;
using TutorFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TutorFlow.Application.Services;

public class StudentService : IStudentService
{
    private readonly IRepository<StudentProfile> _studentRepository;
    private readonly IUserManagementService _userManagementService;
    private readonly ApplicationDbContext _context;

    public StudentService(
        IRepository<StudentProfile> studentRepository, 
        IUserManagementService userManagementService,
        ApplicationDbContext context)
    {
        _studentRepository = studentRepository;
        _userManagementService = userManagementService;
        _context = context;
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

    public async Task<StudentDto?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var student = await _context.StudentProfiles
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        return student?.ToDto();
    }

    public async Task<IReadOnlyList<StudentDto>> GetTutorStudentsAsync(int tutorId, CancellationToken cancellationToken = default)
    {
        var students = await _context.TutorStudentRelations
            .Where(x => x.TutorId == tutorId)
            .Select(x => x.Student!)
            .Include(x => x.User)
            .ToListAsync(cancellationToken);

        return students.Select(x => x.ToDto()).OrderBy(x => x.FullName).ToList();
    }

    public async Task<StudentDto?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        if (user == null) return null;

        var student = await _context.StudentProfiles
            .Include(x => x.User)
            .FirstOrDefaultAsync(s => s.UserId == user.Id, cancellationToken);

        return student?.ToDto();
    }

    public async Task<IEnumerable<StudentDto>> GetByFullNameAsync(string fullName, CancellationToken cancellationToken = default)
    {
        var students = await _context.StudentProfiles
            .Include(x => x.User)
            .Where(x => x.User!.FullName.ToLower().Contains(fullName.ToLower()))
            .ToListAsync(cancellationToken);

        return students.Select(x => x.ToDto());
    }

    public async Task LinkToTutorAsync(int studentId, int tutorId, CancellationToken cancellationToken = default)
    {
        var exists = await _context.TutorStudentRelations
            .AnyAsync(x => x.TutorId == tutorId && x.StudentId == studentId, cancellationToken);

        if (!exists)
        {
            _context.TutorStudentRelations.Add(new TutorStudentRelation
            {
                TutorId = tutorId,
                StudentId = studentId
            });
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task UnlinkFromTutorAsync(int studentId, int tutorId, CancellationToken cancellationToken = default)
    {
        var relation = await _context.TutorStudentRelations
            .FirstOrDefaultAsync(x => x.TutorId == tutorId && x.StudentId == studentId, cancellationToken);

        if (relation != null)
        {
            _context.TutorStudentRelations.Remove(relation);
            await _context.SaveChangesAsync(cancellationToken);
        }
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

