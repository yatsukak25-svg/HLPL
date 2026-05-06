using TutorFlow.Application.DTOs;

namespace TutorFlow.Application.Interfaces;

public interface IStudentService
{
    Task<IReadOnlyList<StudentDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<StudentDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<StudentDto?> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task CreateAsync(StudentDto dto, CancellationToken cancellationToken = default);
    Task UpdateAsync(StudentDto dto, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<StudentDto>> GetTutorStudentsAsync(int tutorId, CancellationToken cancellationToken = default);
    Task<StudentDto?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<StudentDto>> GetByFullNameAsync(string fullName, CancellationToken cancellationToken = default);
    Task LinkToTutorAsync(int studentId, int tutorId, CancellationToken cancellationToken = default);
    Task UnlinkFromTutorAsync(int studentId, int tutorId, CancellationToken cancellationToken = default);
}

