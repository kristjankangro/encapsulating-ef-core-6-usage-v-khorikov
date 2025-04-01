using System.Dynamic;

namespace EFCoreEncapsulation.Api.Repositories;

public class Repository<T> 
	where T : class
	//where T : Entity
{
	protected readonly SchoolContext _context;

	public Repository(SchoolContext context)
	{
		_context = context;
	}

	public virtual T GetById(long id)
	{
		return _context.Set<T>().Find(id);
	}

	public virtual void Save(T entity)
	{
		_context.Set<T>().Add(entity);
	}
	
}