using Issues.Manager.Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Issues.Manager.Application.Services.Identity;

public interface IIdentityManager
{
    Task<IdentityResult> Create(UserRegistrationDto userRegistrationDto);
    Task<bool> ValidateUser(UserLogInDto userForAuth);
    Task<string> CreateToken();

}