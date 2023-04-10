using Microsoft.AspNetCore.Identity;
using TaskMaster.Application.Models;

namespace TaskMaster.Application.Interfaces;

public interface ITokenManager
{
	Task<JwtToken> GenerateToken(IdentityUser? claims);

}