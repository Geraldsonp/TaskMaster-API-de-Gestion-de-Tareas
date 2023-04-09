using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Issues.Manager.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TaskMaster.Application.Models;

namespace Issues.Manager.Application.Services.Token;

public class TokenManager : ITokenManager
{
	private readonly IConfiguration _configurationManager;

	public TokenManager(IConfiguration configurationManager)
	{
		_configurationManager = configurationManager;
	}

	public Task<JwtToken> GenerateToken(IdentityUser? user)
	{
		var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, user.UserName),
			new Claim(ClaimTypes.NameIdentifier, user.Id)
		};

		var signinCredentials = GetSingIngCredentials();
		var token = new JwtSecurityToken
		(
			claims: claims,
			expires:
			DateTime.Now.AddDays(1),
			signingCredentials: signinCredentials
		);
		var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
		return Task.FromResult(new JwtToken
		{
			CreatedAt = DateTime.Now,
			ValidTo = DateTime.Now.AddDays(1),
			Token = tokenString
		});
	}

	private SigningCredentials GetSingIngCredentials()
	{
		var key = Encoding.UTF8.GetBytes(_configurationManager.GetSection("JwtSecretKey").Value);
		var securityKey = new SymmetricSecurityKey(key);
		return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
	}
}