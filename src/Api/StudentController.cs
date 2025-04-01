using EFCoreEncapsulation.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreEncapsulation.Api;

[ApiController]
[Route("students")]
public class StudentController : ControllerBase
{
	private readonly StudentRepository _repository;
	private readonly SchoolContext _context;
	private readonly Repository<Course> _courseRepository;

	public StudentController(StudentRepository repository, 
		SchoolContext context, 
		Repository<Course> courseRepository) //DO NOT use, use CourseRepository instead
	{
		_repository = repository;
		_context = context;
		_courseRepository = courseRepository;
	}

	[HttpGet("{id:long}")]
	public StudentDto Get(long id)
	{
		var student = _repository.GetById(id);
		var course = _courseRepository.GetById(1);
		
		return student?.ToDto();
	}

	[HttpPost]
	public void Register()
	{
		var student = new Student();
		_repository.Save(student);
	}
}