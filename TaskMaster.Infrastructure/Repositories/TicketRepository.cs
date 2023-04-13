using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskMaster.Application.Contracts;
using TaskMaster.Domain.Entities;

namespace TaskMaster.Infrastructure.Repositories;

public class TicketRepository : RepositoryBase<WorkItem>, ITaskEntityRepository
{
	private readonly AppDbContext _dbContext;
	private readonly IAuthenticationStateService _userIdProvider;

	public TicketRepository(AppDbContext dbContext, IAuthenticationStateService userIdProvider) : base(dbContext)
	{
		_dbContext = dbContext;
		_userIdProvider = userIdProvider;
	}

	public WorkItem? FindByCondition(Expression<Func<WorkItem, bool>> expression, bool trackChanges = false)
	{
		return !trackChanges ?
			_dbContext.Tickets
				.Where(ticket => ticket.UserId == _userIdProvider.GetCurrentUserId())
				.Where(expression)
				.AsNoTracking().SingleOrDefault() :
			_dbContext.Set<WorkItem>()
				.Where(ticket => ticket.UserId == _userIdProvider.GetCurrentUserId())
				.Where(expression)
				.SingleOrDefault();
	}

	public IQueryable<WorkItem> FindRangeByCondition(Expression<Func<WorkItem, bool>> expression, bool trackChanges = false)
	{
		return !trackChanges ?
			_dbContext.Set<WorkItem>()
				.Where(ticket => ticket.UserId == _userIdProvider.GetCurrentUserId())
				.Where(expression)
				.AsNoTracking() :
			_dbContext.Set<WorkItem>()
				.Where(expression)
				.Where(ticket => ticket.UserId == _userIdProvider.GetCurrentUserId());
	}

	public IQueryable<WorkItem> FindAll(bool trackChanges = false)
	{
		return !trackChanges
			? _dbContext.Set<WorkItem>()
				.Where(ticket => ticket.UserId == _userIdProvider.GetCurrentUserId())
				.AsNoTracking()
			: _dbContext.Set<WorkItem>()
				.Where(ticket => ticket.UserId == _userIdProvider.GetCurrentUserId());
	}
}