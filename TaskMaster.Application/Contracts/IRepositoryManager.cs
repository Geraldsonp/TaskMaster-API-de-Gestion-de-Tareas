using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Application.Contracts;

public interface IRepositoryManager
{
    ITaskRepository TaskRepository { get; }
    IRepositoryBase<Comment> CommentsRepository { get; }
    void SaveChanges();
}