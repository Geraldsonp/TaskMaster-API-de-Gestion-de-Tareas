using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Domain.Contracts;

public interface IRepositoryManager
{
    IRepositoryBase<Issue> IssuesRepository { get; }
    IRepositoryBase<User> UsersRepository { get; }
    IRepositoryBase<Comment> CommentsRepository { get; }
    void SaveChanges();
}