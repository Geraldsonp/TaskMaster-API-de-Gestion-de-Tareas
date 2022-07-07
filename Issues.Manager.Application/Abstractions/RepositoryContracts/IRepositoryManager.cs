namespace Issues.Manager.Application.Abstractions.RepositoryContracts;

public interface IRepositoryManager
{
    IIssueRepository Issue { get; }
    IUserRepository User { get; }
    void SaveChanges();
}