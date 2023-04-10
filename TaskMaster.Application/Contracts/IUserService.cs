using TaskMaster.Application.Models.User;

namespace TaskMaster.Application.Contracts;

public interface IUserService
{
	string CreateUser(UserRegisterModel user);
	string LogIn(UserLogInModel logInModel, string password);
	void LogOut();
}