using Microsoft.AspNetCore.Identity;

namespace TaskMaster.Application.Models.User;

public class AuthenticationResult
{
	public bool IsSuccess { get; set; } = true;

	public IEnumerable<IdentityError>? Errors { get; set; } = default;

	public JwtToken Token { get; set; }

	public string Error { get; set; }

	public AuthenticationResult(JwtToken token)
	{
		Token = token;
	}

	public AuthenticationResult(IEnumerable<IdentityError> errors)
	{
		Errors = errors;
		IsSuccess = false;
	}

	public AuthenticationResult(bool isSuccess, string errorMessage)
	{
		IsSuccess = isSuccess;
		Error = errorMessage;
	}
}