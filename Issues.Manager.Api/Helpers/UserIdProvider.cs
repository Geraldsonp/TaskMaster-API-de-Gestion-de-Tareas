using System.Security.Claims;
using Issues.Manager.Domain.Contracts;

namespace Issues.Manager.Api.Helpers
{
    public class UserIdProvider : IUserIdProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public UserIdProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;

        }

        public string GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}