using EFCoreEncapsulation.Api.Students;

namespace EFCoreEncapsulation.Api.Courses;

public class Enrollment
{
    public long Id { get; set; }
    public Grade Grade { get; set; }
    public virtual Course Course { get; set; }
    public virtual Student Student { get; set; }
}