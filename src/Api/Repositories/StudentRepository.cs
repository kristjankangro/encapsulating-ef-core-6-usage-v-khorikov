using Microsoft.EntityFrameworkCore;

namespace EFCoreEncapsulation.Api.Repositories;

public class StudentRepository : Repository<Student>
{
	public StudentRepository(SchoolContext context) : base(context)
	{
	}

	//uses auto include in model
	public override Student GetById(long id)
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

	public override void Save(Student student)
	{
		_context.Students.Add(student);
		// if (_context.ChangeTracker.HasChanges()) _context.SaveChanges();
	}

	// public Student Get(long id) => _context.Students.Find(id);
}