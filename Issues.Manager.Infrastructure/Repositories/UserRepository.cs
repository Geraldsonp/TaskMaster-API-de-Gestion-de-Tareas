using Issues.Manager.Application.Abstractions.RepositoryContracts;
using Issues.Manager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Issues.Manager.Infrastructure.Repositories;

public class UserRepository : RepositoryBase<User>,IUserRepository
{

    public UserRepository(AppDbContext dbContext)
        :base(dbContext)
    {

    }
    
   
}