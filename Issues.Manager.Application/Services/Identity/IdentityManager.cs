using AutoMapper;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.Services.Logger;
using Issues.Manager.Domain.Contracts;
using Issues.Manager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Issues.Manager.Application.Services.Identity;

public class IdentityManager : IIdentityManager
{
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;
    private readonly IRepositoryManager _repositoryManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private IdentityUser? _user;

    public IdentityManager(
        ILoggerManager loggerManager,
        IMapper mapper,
        IRepositoryManager repositoryManager,
        UserManager<IdentityUser> userManager,
        IConfiguration configuration)
    {
        _loggerManager = loggerManager;
        _mapper = mapper;
        _repositoryManager = repositoryManager;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<IdentityResult> Create(UserRegisterRequest userRegisterRequest)
    {
        var user = _mapper.Map<IdentityUser>(userRegisterRequest);

        var result = await _userManager.CreateAsync(user, userRegisterRequest.Password);

        if (!result.Succeeded)
        {
            _loggerManager.LogError($"Unable to create IdentityUser");
            return result;
        }

        User appuser = new()
        {
            IdentityId = user.Id,
            FullName = String.Concat($"{userRegisterRequest.FirstName} {userRegisterRequest.LastName}")
        };

        _repositoryManager.UsersRepository.Create(appuser);

        _repositoryManager.SaveChanges();
        return result;
    }

    public async Task<Tuple<bool, IdentityUser>> ValidateUser(UserLogInRequest userForAuth)
    {
        _user = await _userManager.FindByNameAsync(userForAuth.UserName);
        var isValid = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
        
        return new Tuple<bool, IdentityUser>(isValid, _user) ;
    }
    
    
    

}