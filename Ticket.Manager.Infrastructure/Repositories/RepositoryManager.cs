using Issues.Manager.Domain.Contracts;
using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Infrastructure.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly AppDbContext _dbContext;
    private IRepositoryBase<Ticket>? _issueRepository;
    private IRepositoryBase<User>? _userRepository;
    private IRepositoryBase<Comment>? _commentsRepository;

    public RepositoryManager(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IRepositoryBase<Comment>?  CommentsRepository
    {
        get
        {
            if (_commentsRepository == null)
            {
                _commentsRepository = new RepositoryBase<Comment>(_dbContext);
            }

            return _commentsRepository;
        }
        
    }

    public IRepositoryBase<Ticket>?  IssuesRepository
    {
        get
        {
            if (_issueRepository == null)
            {
                _issueRepository = new RepositoryBase<Ticket>(_dbContext);
            }


            return _issueRepository;
        }
    }

    public IRepositoryBase<User>?  UsersRepository
    {
        get
        {
            if (_userRepository == null)
            {
                _userRepository = new RepositoryBase<User>(_dbContext);
            }

            return _userRepository;
        }
    }
    
    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}