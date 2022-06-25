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
        
        var issue = _context.Issues.Add(entity);
        var result = _context.SaveChanges();
        if (result == 0)
        {
            return new Issue();
        }
        return issue.Entity;
    }

    public int Delete(int id)
    {
        var issue = _context.Issues.Find(id);
        if (issue is not null)
        {
            _context.Issues.Remove(issue);
            return _context.SaveChanges();
        }
        else
        {
            throw new NullReferenceException("Issue Does not exist in the database");
        }
        
    }

    public Issue Update(Issue entity)
    {
        
            var result = _context.Issues.Update(entity);
            _context.SaveChanges();
            return result.Entity;
    }

    public Issue GetById(int id)
    {
                
        var issue = _context.Issues.Find(id);
        if (issue is not null)
        {
            return issue; 
        }
        else
            throw new Exception("Issue Not Found in the database");
    }

    public IEnumerable<Issue> GetAll()
    {
        var issues = _context.Issues.ToList();
        return issues;
    }

    public Issue FindByConditionAsync(Expression<Func<Issue, bool>> predicate)
    {
        var issue = _context.Issues.FirstOrDefault(predicate);
        if (issue is not null)
        {
            return issue;
        }
        else
            throw new Exception("Issue Not Found in the database");

    }
}