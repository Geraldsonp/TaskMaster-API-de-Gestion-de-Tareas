using Microsoft.AspNetCore.Identity;

namespace Issues.Manager.Application.DTOs;

public class AuthenticationResult
{
    public bool IsSuccess { get; set; } = true;

    public IEnumerable<IdentityError>? Errors { get; set; } = default;

    public string Token { get; set; }

    public AuthenticationResult(string token)
    {
        Token = token;
    }

    public AuthenticationResult(IEnumerable<IdentityError> errors)
    {
        Errors = errors;
        IsSuccess = false;
    }
}