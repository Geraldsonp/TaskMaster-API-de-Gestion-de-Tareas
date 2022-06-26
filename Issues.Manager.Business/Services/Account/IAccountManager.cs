using Issues.Manager.Business.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Issues.Manager.Business.Services.Account;

public interface IAccountManager
{
    Task<IdentityResult> CreateUser(UserRegistrationDto userRegistrationDto);
    Task<bool> ValidateUser(UserLogInDto userForAuth);
    Task<string> CreateToken();

}