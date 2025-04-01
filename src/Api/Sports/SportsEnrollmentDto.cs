namespace EFCoreEncapsulation.Api.Sports;

public class SportsEnrollmentDto
{
	public long Id { get; set; }
	public string Sports { get; set; }
	public string Student { get; set; }
	public string Grade { get; set; }
}

public static class SportsEnrollmentDtoExtensions
{
	public static SportsEnrollmentDto ToDto(this SportsEnrollment enrollment) =>
		new()
		{
			Grade = enrollment.Grade.ToString(),
			Sports = enrollment.SportId.ToString(), //todo name needed here
			Student = enrollment.Student.Name
		};
}