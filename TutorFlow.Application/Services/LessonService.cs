using TutorFlow.Application.DTOs;
using TutorFlow.Application.Interfaces;
using TutorFlow.Application.Mappings;
using TutorFlow.Application.Specifications;
using TutorFlow.AccountingLibrary;
using TutorFlow.Domain.Entities;
using TutorFlow.Domain.Enums;

namespace TutorFlow.Application.Services;

public class LessonService : ILessonService
{
    private readonly IRepository<Lesson> _lessonRepository;
    private readonly IHolidayApiService _holidayApiService;

    public LessonService(IRepository<Lesson> lessonRepository, IHolidayApiService holidayApiService)
    {
        _lessonRepository = lessonRepository;
        _holidayApiService = holidayApiService;
    }

    public async Task<PagedResult<LessonDto>> GetPagedAsync(
        DateTime? fromDate,
        DateTime? toDate,
        int? studentId,
        int? subjectId,
        LessonStatus? status,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var countSpec = new LessonFilterSpecification(fromDate, toDate, studentId, subjectId, status);
        var listSpec = new LessonFilterSpecification(
            fromDate,
            toDate,
            studentId,
            subjectId,
            status,
            (pageNumber - 1) * pageSize,
            pageSize);

        var totalCount = await _lessonRepository.CountAsync(countSpec, cancellationToken);
        var lessons = await _lessonRepository.ListAsync(listSpec, cancellationToken);

        var items = new List<LessonDto>();
        foreach (var lesson in lessons)
        {
            var dto = lesson.ToDto();
            var holiday = await _holidayApiService.GetHolidayAsync(lesson.StartTime, cancellationToken);
            dto.IsHoliday = holiday.IsHoliday;
            dto.HolidayName = holiday.Name;
            dto.Price = AccountingCalculator.CalculateLessonTotal(dto.Price);
            items.Add(dto);
        }

        return new PagedResult<LessonDto>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    public async Task<IReadOnlyList<LessonDto>> GetTutorLessonsAsync(int tutorId, CancellationToken cancellationToken = default)
    {
        var lessons = await _lessonRepository.ListAsync(new TutorLessonsWithStudentAndSubjectSpecification(tutorId), cancellationToken);
        return lessons.Select(x => x.ToDto()).ToList();
    }

    public async Task<LessonDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var lesson = await _lessonRepository.GetByIdAsync(id, cancellationToken);
        return lesson?.ToDto();
    }

    public async Task CreateAsync(LessonDto dto, CancellationToken cancellationToken = default)
    {
        await _lessonRepository.AddAsync(new Lesson
        {
            TutorId = dto.TutorId,
            StudentId = dto.StudentId,
            SubjectId = dto.SubjectId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Price = AccountingCalculator.CalculateLessonTotal(dto.Price),
            Status = dto.Status,
            Notes = dto.Notes
        }, cancellationToken);
    }

    public async Task UpdateAsync(LessonDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _lessonRepository.GetByIdAsync(dto.Id, cancellationToken)
            ?? throw new InvalidOperationException("Заняття не знайдено.");


        entity.TutorId = dto.TutorId;
        entity.StudentId = dto.StudentId;
        entity.SubjectId = dto.SubjectId;
        entity.StartTime = dto.StartTime;
        entity.EndTime = dto.EndTime;
        entity.Price = AccountingCalculator.CalculateLessonTotal(dto.Price);
        entity.Status = dto.Status;
        entity.Notes = dto.Notes;

        await _lessonRepository.UpdateAsync(entity, cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _lessonRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new InvalidOperationException("Заняття не знайдено.");


        await _lessonRepository.DeleteAsync(entity, cancellationToken);
    }
}

