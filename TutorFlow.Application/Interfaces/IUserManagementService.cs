using TutorFlow.Application.DTOs;

namespace TutorFlow.Application.Interfaces;

public interface IUserManagementService
{
    Task<string> CreateStudentUserAsync(StudentDto dto, CancellationToken cancellationToken = default);
    Task UpdateStudentUserAsync(StudentDto dto, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(string userId, CancellationToken cancellationToken = default);
}

