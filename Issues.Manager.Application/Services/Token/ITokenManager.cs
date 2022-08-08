using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Issues.Manager.Application.Services.Token;

public interface ITokenManager
{
    Task<string> GenerateToken(List<Claim> claims);
    
}