using AutoMapper;
using Issues.Manager.Application.Contracts;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.Interfaces;
using Issues.Manager.Application.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Issues.Manager.Application.Services.Identity;

public class IdentityManager : IIdentityManager
{
    private readonly IMapper _mapper;
    private readonly ITokenManager _tokenManager;
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private IdentityUser? _user;

    public IdentityManager(
        IMapper mapper,
        ITokenManager tokenManager,
        IRepositoryManager repositoryManager,
        UserManager<IdentityUser> userManager,
        IConfiguration configuration)
    {
        _mapper = mapper;
        _tokenManager = tokenManager;
        _repositoryManager = repositoryManager;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<AuthenticationResult> Create(UserRegisterModel userRegisterModel)
    {
        var user = _mapper.Map<IdentityUser>(userRegisterModel);

        var result = await _userManager.CreateAsync(user, userRegisterModel.Password);

        if (!result.Succeeded)
        {
            return new AuthenticationResult(result.Errors);
        }


        var token = await _tokenManager.GenerateToken(user);

        return new AuthenticationResult(token);
    }

    public async Task<AuthenticationResult> LogIn(UserLogInModel userForAuth)
    {
        _user = await _userManager.FindByNameAsync(userForAuth.UserName);

        var isSuccess = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));

        if (!isSuccess)
        {
            return new AuthenticationResult(isSuccess, "UserName Or Password Error") ;
        }

        var token = await _tokenManager.GenerateToken(_user);

        return new AuthenticationResult(token);
    }
}