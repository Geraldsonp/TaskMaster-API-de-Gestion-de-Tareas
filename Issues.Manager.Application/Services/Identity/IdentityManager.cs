using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Issues.Manager.Application.Abstractions.RepositoryContracts;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.Services.Logger;
using Issues.Manager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

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
        _loggerManager.LogInfo($"Creating IdentityUser for: {userRegisterRequest.Email}");
        var result = await _userManager.CreateAsync(user, userRegisterRequest.Password);
        if (!result.Succeeded)
        {
            _loggerManager.LogError($"Unable to create IdentityUser");
            return result;
        }

        _loggerManager.LogInfo($"Identity User Created Successfully");
        User appuser = new()
        {
            IdentityId = user.Id,
            FullName = String.Concat($"{userRegisterRequest.FirstName} {userRegisterRequest.LastName}")
        };
        _repositoryManager.User.Create(appuser);
        _loggerManager.LogInfo($"Creating user for Identity:");
        return result;
    }

    public async Task<bool> ValidateUser(UserLogInRequest userForAuth)
    {
        _user = await _userManager.FindByNameAsync(userForAuth.UserName);
        return (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
    }

    public async Task<string> CreateToken()
    {
        var signinCredentials = GetSingIngCredentials();
        var claims = GetClaims();
        var tokenOptions = GenerateTokenOptions(signinCredentials, claims);
        return  new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private List<Claim> GetClaims()
    {
        var user = _repositoryManager.User.FindByCondition(u => u.IdentityId == _user.Id);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.UserData, user.Id.ToString()),
            new Claim(ClaimTypes.Name, _user.UserName),
            new Claim(ClaimTypes.NameIdentifier, _user.Id)
        };
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken
        (
            claims: claims,
            expires:
            DateTime.Now.AddMinutes(50),
            signingCredentials: signinCredentials
        );
        return tokenOptions;
    }

    private SigningCredentials GetSingIngCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSecretKey").Value);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
}