using System.Security.Claims;
using Issues.Manager.Application.Contracts;

namespace Issues.Manager.Api.Helpers;

public class UserIdProvider : IAuthenticationStateService
{
	private readonly IHttpContextAccessor _contextAccessor;
	public UserIdProvider(IHttpContextAccessor contextAccessor)
	{
		_contextAccessor = contextAccessor;
	}

	public string GetCurrentUserId()
	{
		return _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
	}
}