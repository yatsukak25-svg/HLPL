using TutorFlow.Domain.Common;

namespace TutorFlow.Domain.Entities;

public class TutorStudentRelation : BaseEntity
{
    public int TutorId { get; set; }
    public int StudentId { get; set; }
    
    public TutorProfile? Tutor { get; set; }
    public StudentProfile? Student { get; set; }
}
