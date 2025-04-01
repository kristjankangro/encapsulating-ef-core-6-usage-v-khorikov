using EFCoreEncapsulation.Api.Courses;
using EFCoreEncapsulation.Api.Repositories;
using EFCoreEncapsulation.Api.Students;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreEncapsulation.Api;

[ApiController]
[Route("students")]
public class StudentController : ControllerBase
{
	private readonly StudentRepository _studentRepo;
	private readonly SchoolContext _context;
	private readonly CourseRepository _courseRepo;

	public StudentController(StudentRepository studentRepo, 
		SchoolContext context, 
		CourseRepository courseRepo)
	{
		_studentRepo = studentRepo;
		_context = context;
		_courseRepo = courseRepo;
	}

	[HttpGet("{id:long}")]
	public StudentDto Get(long id)
	{
		var student = _studentRepo.GetById(StudentSpec.All(id));
		
		return student?.ToDto();
	}

	[HttpPost]
	public void Register()
	{
		var student = new Student();
		_studentRepo.Save(student);
	}
	
	[HttpPost]
	public string Enroll(long studentId, long courseId, Grade grade)
	{
		var student = _studentRepo.GetById(studentId);
		if (student == null) return "Student not found";
		
		var course = _courseRepo.GetById(courseId);
		if (course == null) return "Course not found";
		
		string result = student.EnrollIn(course, grade);
		_studentRepo.Save(student);
		return result;
	}
	
	[HttpPost]
	public string EditPersonalInfo(long studentId, string name, string email)
	{
		var student = _studentRepo.GetById(studentId);
		if (student == null) return "Student not found";
		
		student.Name = name;
		student.Email = email;
		_studentRepo.Save(student);
		return "Ok";
	}
}