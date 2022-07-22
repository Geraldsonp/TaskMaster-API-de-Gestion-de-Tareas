using Issues.Manager.Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Issues.Manager.Application.Services.Identity;

public interface IIdentityManager
{
    Task<IdentityResult> Create(UserRegisterRequest userRegisterRequest);
    Task<bool> ValidateUser(UserLogInRequest userForAuth);
    Task<string> CreateToken();

}