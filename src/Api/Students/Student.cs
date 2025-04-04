﻿using EFCoreEncapsulation.Api.Courses;
using EFCoreEncapsulation.Api.Shared;
using EFCoreEncapsulation.Api.Sports;

namespace EFCoreEncapsulation.Api.Students;

public class Student
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public ICollection<Enrollment> Enrollments { get; set; }
	public ICollection<SportsEnrollment> SportsEnrollments { get; set; }

	public string EnrollIn(Course course, Grade grade)
	{
		if (Enrollments.Any(x => x.CourseId == course.Id))
		{
			return "Already enrolled in this course";
		}

		var enrollment = new Enrollment
		{
			CourseId = course.Id,
			Student = this,
			Grade = grade
		};
		Enrollments.Add(enrollment);
		return "Ok";
	}
}