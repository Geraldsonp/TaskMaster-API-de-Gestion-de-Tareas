using Issues.Manager.Application.Models.User;
using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Application.Contracts;

public interface IUserService
{
    string CreateUser(UserRegisterModel user);
    string LogIn(UserLogInModel logInModel, string password);
    void LogOut();
}