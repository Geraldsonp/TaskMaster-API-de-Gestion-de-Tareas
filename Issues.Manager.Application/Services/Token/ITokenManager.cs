using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Issues.Manager.Application.Services.Token;

public interface ITokenManager
{
    Task<string> GenerateToken(IdentityUser? claims);
    
}