using EFCoreEncapsulation.Api.Courses;
using EFCoreEncapsulation.Api.Sports;

namespace EFCoreEncapsulation.Api.Students;

public class StudentDto
{
    public long StudentId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    
    public ICollection<EnrollmentDto> Enrollments { get; set; }
    public ICollection<SportsEnrollmentDto> SportsEnrollments { get; set; }
}

public static class StudentExtensions
{
	public static StudentDto ToDto(this Student student) =>
		new()
		{
			StudentId = student.Id,
			Name = student.Name,
			Email = student.Email,
			Enrollments = student.Enrollments.Select(x => x.ToDto()).ToList(),
			SportsEnrollments = student.SportsEnrollments.Select(x=> x.ToDto()).ToList()
		};
}