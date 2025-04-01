using EFCoreEncapsulation.Api.Repositories;

namespace EFCoreEncapsulation.Api;

public class CourseRepository : Repository<Course>
{
	public CourseRepository(SchoolContext context): base(context)
	{
	}
}