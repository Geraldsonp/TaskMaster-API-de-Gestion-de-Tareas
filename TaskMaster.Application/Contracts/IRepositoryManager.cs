using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Application.Contracts;

public interface IUnitOfWork
{
	ITaskEntityRepository TaskRepository { get; }
	IRepositoryBase<Comment> CommentsRepository { get; }
	void SaveChanges();
}