using TutorFlow.Domain.Entities;
using TutorFlow.Domain.Enums;

namespace TutorFlow.Application.Specifications;

public class LessonFilterSpecification : BaseSpecification<Lesson>
{
    public LessonFilterSpecification(
        DateTime? fromDate,
        DateTime? toDate,
        int? studentId,
        int? subjectId,
        LessonStatus? status,
        int? skip = null,
        int? take = null)
        : base(lesson =>
            (!fromDate.HasValue || lesson.StartTime.Date >= fromDate.Value.Date) &&
            (!toDate.HasValue || lesson.StartTime.Date <= toDate.Value.Date) &&
            (!studentId.HasValue || lesson.StudentId == studentId.Value) &&
            (!subjectId.HasValue || lesson.SubjectId == subjectId.Value) &&
            (!status.HasValue || lesson.Status == status.Value))
    {
        AddInclude(x => x.Student!);
        AddInclude(x => x.Subject!);
        AddInclude(x => x.Tutor!);
        AddInclude(x => x.Payment!);
        ApplyOrderBy(x => x.StartTime);

        if (skip.HasValue && take.HasValue)
        {
            ApplyPaging(skip.Value, take.Value);
        }
    }
}

