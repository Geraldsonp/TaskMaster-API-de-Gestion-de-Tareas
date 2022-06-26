using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Issues.Manager.Business.Abstractions.LoggerContract;
using Issues.Manager.Business.Abstractions.RepositoryContracts;
using Issues.Manager.Business.DTOs;
using Issues.Manager.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Issues.Manager.Business.Services.Account;

public class AccountManager : IAccountManager
{
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private IdentityUser _user;

    public AccountManager(ILoggerManager loggerManager, IMapper mapper, 
        UserManager<IdentityUser> userManager, 
        IUserRepository userRepository,
        IConfiguration configuration)
    {
        _loggerManager = loggerManager;
        _mapper = mapper;
        _userManager = userManager;
        _userRepository = userRepository;
        _configuration = configuration;
    }
    
    public async Task<IdentityResult> CreateUser(UserRegistrationDto userRegistrationDto)
    {

        var user = _mapper.Map<IdentityUser>(userRegistrationDto);
        _loggerManager.LogError($"Creating IdentityUser for: {userRegistrationDto.Email}");
        var result = await _userManager.CreateAsync(user, userRegistrationDto.Password);
        if (!result.Succeeded)
        {
            _loggerManager.LogError($"Unable to create IdentityUser");
            return result;
        }
        
        _loggerManager.LogError($"Identity User Created Successfully");
        var claims = await  _userManager.GetClaimsAsync(user);
        User appuser = new()
        {
            IdentityId = user.Id,
            FullName = String.Concat($"{userRegistrationDto.FirstName} {userRegistrationDto.LastName}")
        };
        _userRepository.Create(appuser);
        _loggerManager.LogError($"Creating user for Identity:");
        return result;



    }

    public async Task<bool> ValidateUser(UserLogInDto userForAuth)
    {
        _user = await _userManager.FindByNameAsync(userForAuth.UserName);
        return (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));
    }

    public async Task<string> CreateToken()
    {
        var signinCredentials = GetSingIngCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signinCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);

    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.UserName),
            new Claim(ClaimTypes.NameIdentifier, _user.Id)
        };
        // var roles = await _userManager.GetRolesAsync(_user);
        // foreach (var role in roles)
        // {
        //     claims.Add(new Claim(ClaimTypes.Role, role));
        // }
        return claims;

    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials,  List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
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