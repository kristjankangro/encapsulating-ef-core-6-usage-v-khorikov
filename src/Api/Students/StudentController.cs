using EFCoreEncapsulation.Api.Contexts;
using EFCoreEncapsulation.Api.Courses;
using EFCoreEncapsulation.Api.Repositories;
using EFCoreEncapsulation.Api.Shared;
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

	[HttpGet]
	public IReadOnlyList<StudentDto> GetAll()
	{
		return _studentRepo
			.GetAll(true)
			.Select(MapToDto)
			.ToList();
	}

	private StudentDto MapToDto(Student student)
	{
		return null;
	}

	[HttpGet("{id:long}")]
	public StudentDto Get(long id)
	{
		return _studentRepo.GetDto(id);
	}

	[HttpPost("register")]
	public void Register()
	{
		var student = new Student();
		_studentRepo.Save(student);
	}
	
	[HttpPost("enroll")]
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
	
	[HttpPost("update")]
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