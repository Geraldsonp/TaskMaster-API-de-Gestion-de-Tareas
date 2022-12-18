using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Domain.Contracts;

public interface IUserService
{
    string CreateUser(User user);
    string LogIn(User user, string password);
    void LogOut();
}