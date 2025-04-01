using EFCoreEncapsulation.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EFCoreEncapsulation.Api.Students;

public class StudentRepository : Repository<Student>
{
	public StudentRepository(SchoolContext context) : base(context)
	{
	}

	public Student GetById(StudentSpec spec)
	{
		var student = _context.Students.Find(spec.Id);
		if (student == null) return null;

		if (spec.WithEnrollments) 
			_context.Entry(student).Collection(s => s.Enrollments).Load();
		if (spec.WithSportEnrollments) 
			_context.Entry(student).Collection(s => s.SportsEnrollments).Load();

		return student;
	}

	public Student GetByIdSpiltQueries(long id)
	{
		var student = _context.Students
			.Include(s => s.Enrollments)
			.ThenInclude(e => e.Course)
			.Include(s => s.SportsEnrollments)
			.ThenInclude(s => s.Sport)
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