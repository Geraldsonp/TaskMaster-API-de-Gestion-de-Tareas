using System.Linq.Expressions;
using Issues.Manager.Business.Abstractions.RepositoryContracts;
using Issues.Manager.Domain.Entities;

namespace Issues.Manager.DataAccess.Repositories;

public class IssueRepository : IRepositoryBase<Issue>
{
    private readonly AppDbContext _context;

    public IssueRepository(AppDbContext context)
    {
        _context = context;
    }
    public Issue Create(Issue entity)
    {
        throw new NotImplementedException();
    }

    public int Delete(Issue entity)
    {
        throw new NotImplementedException();
    }

    public Issue Update(Issue entity)
    {
        throw new NotImplementedException();
    }

    public Issue GetById(Issue entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Issue> GetAll()
    {
        throw new NotImplementedException();
    }

    public Issue FindByConditionAsync(Expression<Func<Issue, bool>> predicate)
    {
        throw new NotImplementedException();
    }
}