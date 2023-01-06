using AutoMapper;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.Services.Token;
using Issues.Manager.Domain.Contracts;
using Issues.Manager.Domain.Entities;
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

    public async Task<AuthenticationResult> Create(UserRegisterRequest userRegisterRequest)
    {
        var user = _mapper.Map<IdentityUser>(userRegisterRequest);

        var result = await _userManager.CreateAsync(user, userRegisterRequest.Password);

        if (!result.Succeeded)
        {
            return new AuthenticationResult(result.Errors);
        }

        User appuser = new()
        {
            IdentityId = user.Id,
            FullName = String.Concat($"{userRegisterRequest.FirstName} {userRegisterRequest.LastName}")
        };

        _repositoryManager.UsersRepository.Create(appuser);
        _repositoryManager.SaveChanges();

        var token = await _tokenManager.GenerateToken(user);

        return new AuthenticationResult(token);
    }

    public async Task<AuthenticationResult> LogIn(UserLogInRequest userForAuth)
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