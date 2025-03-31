using Microsoft.EntityFrameworkCore;

namespace EFCoreEncapsulation.Api;

public class StudentRepository
{
	private readonly SchoolContext _context;

	public StudentRepository(SchoolContext context)
	{
		_context = context;
	}

	//uses auto include in model
	public Student Get(long id)
	{
		var student = _context.Students.Find(id);
		if (student == null) return null;
		
		_context.Entry(student).Collection(s => s.Enrollments).Load();
		_context.Entry(student).Collection(s => s.SportsEnrollments).Load();

		return student;
	}

	public Student GetByIdSpiltQueries(long id)
	{
		var student = _context.Students
			.Include(s => s.Enrollments)
			.ThenInclude(e => e.Course)
			.Include(s => s.SportsEnrollments)
			.ThenInclude(s => s.Sports)
			.AsSplitQuery()
			.SingleOrDefault(s => s.Id == id);

		return student;
	}

	// public Student Get(long id) => _context.Students.Find(id);
}