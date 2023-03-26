using Microsoft.AspNetCore.Identity;

namespace Issues.Manager.Application.Interfaces;

public interface ITokenManager
{
    Task<string> GenerateToken(IdentityUser? claims);
    
}