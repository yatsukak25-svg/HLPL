using TutorFlow.Domain.Entities;

namespace TutorFlow.Application.Specifications;

public class TutorLessonsWithStudentAndSubjectSpecification : BaseSpecification<Lesson>
{
    public TutorLessonsWithStudentAndSubjectSpecification(int tutorId)
        : base(lesson => lesson.TutorId == tutorId)
    {
        AddInclude(x => x.Student!);
        AddInclude(x => x.Subject!);
        AddInclude(x => x.Tutor!);
        AddInclude(x => x.Payment!);
        ApplyOrderBy(x => x.StartTime);
    }
}

