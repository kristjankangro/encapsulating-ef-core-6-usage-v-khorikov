using System.Dynamic;
using EFCoreEncapsulation.Api.Contexts;

namespace EFCoreEncapsulation.Api.Repositories;

public abstract class Repository<T> 
	where T : class
	//where T : Entity : AggregateRoot
{
	protected readonly SchoolContext _context;

	protected Repository(SchoolContext context) => _context = context;

	public virtual T GetById(long id) => _context.Set<T>().Find(id);

	public virtual void Save(T entity) => _context.Set<T>().Add(entity);
}