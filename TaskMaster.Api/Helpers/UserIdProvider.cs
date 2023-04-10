using System.Security.Claims;
using TaskMaster.Application.Contracts;

namespace TaskMaster.Api.Helpers;

public class UserIdProvider : IAuthenticationStateService
{
	private readonly IHttpContextAccessor _contextAccessor;
	public UserIdProvider(IHttpContextAccessor contextAccessor)
	{
		_contextAccessor = contextAccessor;
	}

	public string? GetCurrentUserId()
	{
		return _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null;
	}
}