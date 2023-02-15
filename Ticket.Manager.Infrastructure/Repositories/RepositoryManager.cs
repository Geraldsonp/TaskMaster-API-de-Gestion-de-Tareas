using Issues.Manager.Application.Contracts;
using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Infrastructure.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly AppDbContext _dbContext;
    private readonly IUserIdProvider _userIdProvider;
    private ITaskRepository? _ticketRepository;
    private IRepositoryBase<Comment>? _commentsRepository;

    public RepositoryManager(AppDbContext dbContext, IUserIdProvider userIdProvider)
    {
        _dbContext = dbContext;
        _userIdProvider = userIdProvider;
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

    public ITaskRepository?  TaskRepository
    {
        get
        {
            if (_ticketRepository == null)
            {
                _ticketRepository = new TicketRepository(_dbContext, _userIdProvider);
            }


            return _ticketRepository;
        }
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}