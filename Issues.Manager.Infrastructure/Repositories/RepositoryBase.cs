using System.Linq.Expressions;
using Issues.Manager.Domain.Contracts;
using Issues.Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Issues.Manager.Infrastructure.Repositories;


public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
{
    private AppDbContext _dbContext;

    public RepositoryBase(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Create(T entity)
    { 
         _dbContext.Set<T>().Add(entity);
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
    {
        return !trackChanges ?
            _dbContext.Set<T>()
                .Where(expression)
                .AsNoTracking().SingleOrDefault() :
            _dbContext.Set<T>()
                .Where(expression).SingleOrDefault(); 
    }

    public IQueryable<T> FindRangeByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false)
    {
        return !trackChanges ?
            _dbContext.Set<T>()
                .Where(expression)
                .AsNoTracking() :
            _dbContext.Set<T>()
                .Where(expression); 
    }

    public IQueryable<T> FindAll(bool trackChanges = false)
    {
        return !trackChanges
            ? _dbContext.Set<T>()
                .AsNoTracking()
            : _dbContext.Set<T>();
    }
}