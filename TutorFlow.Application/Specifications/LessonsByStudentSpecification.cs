using TutorFlow.Domain.Entities;

namespace TutorFlow.Application.Specifications;

public class LessonsByStudentSpecification : BaseSpecification<Lesson>
{
    public LessonsByStudentSpecification(int studentId)
        : base(lesson => lesson.StudentId == studentId)
    {
        AddInclude(x => x.Student!);
        AddInclude(x => x.Subject!);
        AddInclude(x => x.Tutor!);
        ApplyOrderBy(x => x.StartTime);
    }
}

