using Issues.Manager.Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Issues.Manager.Application.Services.Identity;

public interface IIdentityManager
{
    Task<AuthenticationResult> Create(UserRegisterRequest userRegisterRequest);
    Task<Tuple<bool, string>> LogIn(UserLogInRequest userForAuth);
}