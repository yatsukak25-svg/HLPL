using TutorFlow.Application.DTOs;
using TutorFlow.Domain.Entities;

namespace TutorFlow.Application.Mappings;

public static class EntityMappings
{
    public static StudentDto ToDto(this StudentProfile entity)
    {
        return new StudentDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            FullName = entity.User?.FullName ?? string.Empty,
            Email = entity.User?.Email ?? string.Empty,
            PhoneNumber = entity.User?.PhoneNumber,
            Grade = entity.Grade,
            Notes = entity.Notes
        };
    }

    public static SubjectDto ToDto(this Subject entity)
    {
        return new SubjectDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description
        };
    }

    public static LessonDto ToDto(this Lesson entity)
    {
        return new LessonDto
        {
            Id = entity.Id,
            TutorId = entity.TutorId,
            StudentId = entity.StudentId,
            SubjectId = entity.SubjectId,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            Price = entity.Price,
            Status = entity.Status,
            Notes = entity.Notes,
            TutorName = entity.Tutor?.User?.FullName ?? string.Empty,
            StudentName = entity.Student?.User?.FullName ?? string.Empty,
            SubjectName = entity.Subject?.Name ?? string.Empty
        };
    }

    public static HomeworkDto ToDto(this Homework entity)
    {
        return new HomeworkDto
        {
            Id = entity.Id,
            LessonId = entity.LessonId,
            Title = entity.Title,
            Description = entity.Description,
            Deadline = entity.Deadline,
            Status = entity.Status,
            FileUrl = entity.FileUrl,
            LessonSummary = entity.Lesson is null
                ? string.Empty
                : $"{entity.Lesson.StartTime:g} / {entity.Lesson.Subject?.Name}"
        };
    }

    public static PaymentDto ToDto(this Payment entity)
    {
        return new PaymentDto
        {
            Id = entity.Id,
            LessonId = entity.LessonId,
            Amount = entity.Amount,
            IsPaid = entity.IsPaid,
            PaymentDate = entity.PaymentDate,
            Comment = entity.Comment,
            LessonSummary = entity.Lesson is null
                ? string.Empty
                : $"{entity.Lesson.StartTime:g} / {entity.Lesson.Subject?.Name}",
            StudentName = entity.Lesson?.Student?.User?.FullName ?? string.Empty
        };
    }

    public static LearningMaterialDto ToDto(this LearningMaterial entity)
    {
        return new LearningMaterialDto
        {
            Id = entity.Id,
            SubjectId = entity.SubjectId,
            Title = entity.Title,
            Description = entity.Description,
            Url = entity.Url,
            SubjectName = entity.Subject?.Name ?? string.Empty
        };
    }
}

