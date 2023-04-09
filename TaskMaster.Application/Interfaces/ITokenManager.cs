using Microsoft.AspNetCore.Identity;
using TaskMaster.Application.Models;

namespace Issues.Manager.Application.Interfaces;

public interface ITokenManager
{
	Task<JwtToken> GenerateToken(IdentityUser? claims);

}