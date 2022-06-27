using Issues.Manager.Business.Abstractions.RepositoryContracts;
using Issues.Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Issues.Manager.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public void Create(User entity)
    {
        _dbContext.Users.Add(entity);
        _dbContext.SaveChanges();

    }
    
    public User GetById(string id)
    {
        return _dbContext.Users
            .Include(i => i.IssuesCreated)
            .First(u => u.IdentityId == id);
    }

    public bool SaveChanges()
    {
       var result =  _dbContext.SaveChanges();
       if (result <= 0)
       {
           return false;
       }

       return true;
    }
}