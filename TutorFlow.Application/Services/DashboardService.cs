using TutorFlow.Application.DTOs;
using TutorFlow.Application.Interfaces;
using TutorFlow.Application.Specifications;
using TutorFlow.AccountingLibrary;
using TutorFlow.Domain.Entities;
using TutorFlow.Domain.Enums;

namespace TutorFlow.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IRepository<StudentProfile> _studentRepository;
    private readonly IRepository<Lesson> _lessonRepository;
    private readonly IRepository<Homework> _homeworkRepository;
    private readonly IRepository<Payment> _paymentRepository;

    public DashboardService(
        IRepository<StudentProfile> studentRepository,
        IRepository<Lesson> lessonRepository,
        IRepository<Homework> homeworkRepository,
        IRepository<Payment> paymentRepository)
    {
        _studentRepository = studentRepository;
        _lessonRepository = lessonRepository;
        _homeworkRepository = homeworkRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<DashboardStatsDto> GetTutorDashboardAsync(int tutorId, CancellationToken cancellationToken = default)
    {
        var lessons = await _lessonRepository.ListAsync(new TutorLessonsWithStudentAndSubjectSpecification(tutorId), cancellationToken);
        var homework = await _homeworkRepository.ListAsync(cancellationToken);
        var payments = lessons.Where(x => x.Payment is not null).Select(x => x.Payment!).ToList();

        var now = DateTime.UtcNow;
        return new DashboardStatsDto
        {
            TotalStudents = lessons.Select(x => x.StudentId).Distinct().Count(),
            PlannedLessons = lessons.Count(x => x.Status == LessonStatus.Planned),
            PendingHomework = homework.Count(x => x.Status != HomeworkStatus.Checked),
            MonthlyIncome = AccountingCalculator.CalculateMonthlyIncome(
                payments.Where(x => x.IsPaid && x.PaymentDate.HasValue && x.PaymentDate.Value.Year == now.Year && x.PaymentDate.Value.Month == now.Month)
                    .Select(x => x.Amount)),
            OutstandingDebt = AccountingCalculator.CalculateStudentDebt(payments.Select(x => (x.Amount, x.IsPaid)))
        };
    }

    public async Task<DashboardStatsDto> GetStudentDashboardAsync(int studentId, CancellationToken cancellationToken = default)
    {
        var lessons = await _lessonRepository.ListAsync(new LessonsByStudentSpecification(studentId), cancellationToken);
        var homework = await _homeworkRepository.ListAsync(cancellationToken);
        var payments = await _paymentRepository.ListAsync(cancellationToken);

        return new DashboardStatsDto
        {
            TotalStudents = 1,
            PlannedLessons = lessons.Count(x => x.Status == LessonStatus.Planned),
            PendingHomework = homework.Count(x => x.Lesson?.StudentId == studentId && x.Status != HomeworkStatus.Checked),
            MonthlyIncome = 0,
            OutstandingDebt = AccountingCalculator.CalculateStudentDebt(
                payments.Where(x => x.Lesson?.StudentId == studentId).Select(x => (x.Amount, x.IsPaid)))
        };
    }
}

