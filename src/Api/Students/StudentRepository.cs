using EFCoreEncapsulation.Api.Contexts;
using EFCoreEncapsulation.Api.Courses;
using EFCoreEncapsulation.Api.Repositories;
using EFCoreEncapsulation.Api.Shared;
using Microsoft.EntityFrameworkCore;

namespace EFCoreEncapsulation.Api.Students;

public class StudentRepository : Repository<Student>
{
	public StudentRepository(SchoolContext context) : base(context)
	{
	}

	public StudentDto GetDto(long id)
	{
		var student = _context.Students.Find(id);
		if (student == null) return null;

		List<EnrollmentData> enrollments = _context.Set<EnrollmentData>()
			.FromSqlInterpolated($@"
				SELECT e.StudentID, e.Grade, c.Name as Course
				FROM dbo.Enrollments e
				inner join dbo.Course c on c.CourseID = e.CourseID
				WHERE StudentId = {id}")
			.ToList();
		var dto = student.ToDto();
		dto.Enrollments = enrollments.Select(ToDto()).ToList();
		return dto;
	}

	private static Func<EnrollmentData, EnrollmentDto> ToDto()
	{
		return x => new EnrollmentDto()
		{
			Grade = ((Grade)x.Grade).ToString(),
			Course = x.Course
		};
	}

	public override Student GetById(long id)
	{
		var student = _context.Students.Find(id);
		if (student == null) return null;

		_context.Entry(student).Collection(s => s.Enrollments).Load();
		_context.Entry(student).Collection(s => s.SportsEnrollments).Load();

		return student;
	}

	// public Student GetById(StudentSpec spec)
	// {
	// 	var student = _context.Students.Find(spec.Id);
	// 	if (student == null) return null;
	//
	// 	if (spec.WithEnrollments)
	// 		_context.Entry(student).Collection(s => s.Enrollments).Load();
	// 	if (spec.WithSportEnrollments)
	// 		_context.Entry(student).Collection(s => s.SportsEnrollments).Load();
	//
	// 	return student;
	// }

	// public Student GetByIdSpiltQueries(long id)
	// {
	// 	var student = _context.Students
	// 		.Include(s => s.Enrollments)
	// 		.ThenInclude(e => e.Course)
	// 		.Include(s => s.SportsEnrollments)
	// 		.ThenInclude(s => s.Sport)
	// 		.AsSplitQuery()
	// 		.SingleOrDefault(s => s.Id == id);
	//
	// 	return student;
	// }

	public override void Save(Student student)
	{
		_context.Students.Add(student);
		// if (_context.ChangeTracker.HasChanges()) _context.SaveChanges();
	}

	// public Student Get(long id) => _context.Students.Find(id);
}