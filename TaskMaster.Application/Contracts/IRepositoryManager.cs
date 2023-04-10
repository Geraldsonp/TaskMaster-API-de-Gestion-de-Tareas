using TaskMaster.Domain.Entities;

namespace TaskMaster.Application.Contracts;

public interface IUnitOfWork
{
	ITaskEntityRepository TaskRepository { get; }
	IRepositoryBase<Comment> CommentsRepository { get; }
	void SaveChanges();
}