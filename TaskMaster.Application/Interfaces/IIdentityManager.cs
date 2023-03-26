using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.Models.User;

namespace Issues.Manager.Application.Services.Identity;

public interface IIdentityManager
{
    Task<AuthenticationResult> Create(UserRegisterModel userRegisterModel);
    Task<AuthenticationResult> LogIn(UserLogInModel userForAuth);
}