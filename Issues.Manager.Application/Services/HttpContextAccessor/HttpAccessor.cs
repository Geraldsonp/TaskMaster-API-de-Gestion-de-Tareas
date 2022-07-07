using System.Security.Claims;
using Issues.Manager.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Issues.Manager.Application.Services.HttpContextAccessor;

public class HttpAccessor : IHttpAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;


    public HttpAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        
    }

    public int GetCurrentIdentityId()
    {
        if (_httpContextAccessor.HttpContext != null)
        {
            var user = _httpContextAccessor.HttpContext.User;
            var id = user.FindFirstValue(ClaimTypes.UserData);
            if (id != null)
            {
                return Int32.Parse(id);
            }
            return 0;
        }
        else
        {
            return 0;
        }
    }


}