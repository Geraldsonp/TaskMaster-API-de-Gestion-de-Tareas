using System.Linq.Expressions;
using TaskMaster.Domain.Entities;

namespace TaskMaster.Application.Contracts;

public interface IRepositoryBase<T> where T : BaseEntity
{
	void Create(T entity);
	void Delete(T entity);
	void Update(T entity);
	T FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
	IQueryable<T> FindRangeByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
	IQueryable<T> FindAll(bool trackChanges = false);
}