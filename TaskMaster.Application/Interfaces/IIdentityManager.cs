using TaskMaster.Application.Models.User;

namespace TaskMaster.Application.Interfaces;

public interface IIdentityManager
{
	Task<AuthenticationResult> Create(UserRegisterModel userRegisterModel);
	Task<AuthenticationResult> LogIn(UserLogInModel userForAuth);
}