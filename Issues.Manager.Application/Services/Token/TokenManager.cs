using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Issues.Manager.Application.Services.Token;

public class TokenManager : ITokenManager
{
    private readonly IConfiguration _configurationManager;

    public TokenManager(IConfiguration configurationManager)
    {
        _configurationManager = configurationManager;
    }
    
    public Task<string> GenerateToken( List<Claim> claims)
    {
        var signinCredentials = GetSingIngCredentials();
        var token = new JwtSecurityToken
        (
            claims: claims,
            expires:
            DateTime.Now.AddDays(1),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return Task.FromResult(tokenString);

    }
    
    private SigningCredentials GetSingIngCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_configurationManager.GetSection("JwtSecretKey").Value);
        var securityKey = new SymmetricSecurityKey(key);
        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    }
}