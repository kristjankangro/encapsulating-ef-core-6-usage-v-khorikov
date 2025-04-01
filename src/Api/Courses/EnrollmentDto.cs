namespace EFCoreEncapsulation.Api.Courses;

public class EnrollmentDto
{
	public string Grade { get; set; }
	public string Course { get; set; }
	
}

public static class EnrollmentDtoExtensions
{
	public static EnrollmentDto ToDto(this Enrollment enrollment) =>
		new()
		{
			Grade = enrollment.Grade.ToString(),
			Course = enrollment.Course.Name
		};
}