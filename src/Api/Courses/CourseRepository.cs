using EFCoreEncapsulation.Api.Contexts;
using EFCoreEncapsulation.Api.Repositories;

namespace EFCoreEncapsulation.Api.Courses;

public class CourseRepository : Repository<Course>
{
	public CourseRepository(SchoolContext context): base(context)
	{
	}
}