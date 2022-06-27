using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Business.Abstractions.RepositoryContracts;

public interface IUserRepository
{
    void Create(User entity);
    User GetById(string id);
    bool SaveChanges();
}