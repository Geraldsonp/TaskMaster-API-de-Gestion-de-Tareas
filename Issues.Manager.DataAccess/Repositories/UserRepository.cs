using Issues.Manager.Business.Abstractions.RepositoryContracts;
using Issues.Manager.Domain.Entities;

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
    
    public User GetById(int id)
    {
        return _dbContext.Users.Find(id);
    }




}