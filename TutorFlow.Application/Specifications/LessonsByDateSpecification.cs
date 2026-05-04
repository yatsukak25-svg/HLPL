using TutorFlow.Domain.Entities;

namespace TutorFlow.Application.Specifications;

public class LessonsByDateSpecification : BaseSpecification<Lesson>
{
    public LessonsByDateSpecification(DateTime? fromDate, DateTime? toDate)
        : base(lesson =>
            (!fromDate.HasValue || lesson.StartTime.Date >= fromDate.Value.Date) &&
            (!toDate.HasValue || lesson.StartTime.Date <= toDate.Value.Date))
    {
        AddInclude(x => x.Student!);
        AddInclude(x => x.Subject!);
        AddInclude(x => x.Tutor!);
        ApplyOrderBy(x => x.StartTime);
    }
}

