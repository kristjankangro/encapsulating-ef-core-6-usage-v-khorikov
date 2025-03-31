using Microsoft.AspNetCore.Mvc;

namespace EFCoreEncapsulation.Api;

[ApiController]
[Route("students")]
public class StudentController : ControllerBase
{
	private readonly StudentRepository _repository;

	public StudentController(StudentRepository repository)
	{
		_repository = repository;
	}

	[HttpGet("{id:long}")]
	public StudentDto Get(long id)
	{
		var student = _repository.Get(id);
		
		return student?.ToDto();
	}
}