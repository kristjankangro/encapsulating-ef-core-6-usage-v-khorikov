using Microsoft.AspNetCore.Mvc;

namespace EFCoreEncapsulation.Api;

[ApiController]
[Route("students")]
public class StudentController : ControllerBase
{
	private readonly SchoolContext _context;

	public StudentController(SchoolContext context)
	{
		_context = context;
	}

	[HttpGet("{id}")]
	public StudentDto Get(long id)
	{
		var student = _context.Students.Find(id);
		if (student == null) return null;

		return new StudentDto
		{
			StudentId = student.Id,
			Name = student.Name,
			Email = student.Email,
			Enrollments = student.Enrollments.Select(e =>
				new EnrollmentDto()
				{
					Grade = e.Grade.ToString(),
					Course = e.Course.Name
				}).ToList()
		};
	}
}