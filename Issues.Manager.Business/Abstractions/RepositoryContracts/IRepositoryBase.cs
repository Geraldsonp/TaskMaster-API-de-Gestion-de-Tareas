using System.Linq.Expressions;
using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Business.Abstractions.RepositoryContracts;

public interface IRepositoryBase<T> where T : BaseEntity
{
    T Create(T entity);
    int Delete(T entity);
    T Update(T entity);
    T GetById(int id);
    IEnumerable<T> GetAll(int userId);
    T FindByConditionAsync(Expression<Func<T, bool>> predicate);
}