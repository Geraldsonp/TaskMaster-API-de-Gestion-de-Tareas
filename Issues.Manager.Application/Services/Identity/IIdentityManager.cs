using Issues.Manager.Application.DTOs;
using Microsoft.AspNetCore.Identity;

namespace Issues.Manager.Application.Services.Identity;

public interface IIdentityManager
{
    Task<IdentityResult> Create(UserRegisterRequest userRegisterRequest);
    Task<Tuple<bool, IdentityUser>> ValidateUser(UserLogInRequest userForAuth);
}