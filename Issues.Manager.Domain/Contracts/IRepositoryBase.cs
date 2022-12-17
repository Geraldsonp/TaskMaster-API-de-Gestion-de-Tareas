using System.Linq.Expressions;
using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Domain.Contracts;

public interface IRepositoryBase<T> where T : BaseEntity
{
    void Create(T entity);
    void Delete(T entity);
    void Update(T entity);
    T FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
    IQueryable<T> FindRangeByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
    IQueryable<T> FindAll(bool trackChanges = false);
}