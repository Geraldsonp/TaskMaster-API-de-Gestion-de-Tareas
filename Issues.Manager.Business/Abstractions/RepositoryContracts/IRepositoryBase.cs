using System.Linq.Expressions;
using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Business.Abstractions.RepositoryContracts;

public interface IRepositoryBase<T> where T : BaseEntity
{
    T Create(T entity);
    int Delete(int id);
    T Update(T entity);
    T GetById(int id);
    IEnumerable<T> GetAll();
    T FindByConditionAsync(Expression<Func<T, bool>> predicate);
}