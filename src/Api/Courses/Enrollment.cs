using EFCoreEncapsulation.Api.Shared;
using EFCoreEncapsulation.Api.Students;

namespace EFCoreEncapsulation.Api.Courses;

public class Enrollment
{
    public long Id { get; set; }
    public Grade Grade { get; set; }
    // public Course Course { get; set; }
    public long CourseId { get; set; } //for performance reasons
    public Student Student { get; set; }
}