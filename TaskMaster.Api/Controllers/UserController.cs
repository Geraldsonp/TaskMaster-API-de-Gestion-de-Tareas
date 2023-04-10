using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.Api.Contracts.Responses;
using TaskMaster.Application.Interfaces;
using TaskMaster.Application.Models;
using TaskMaster.Application.Models.User;

namespace TaskMaster.Api.Controllers;

[Route("api/[controller]")]
[AllowAnonymous]
[ApiController]
public class UserController : ControllerBase
{
	private readonly IIdentityManager _identityManager;

	public UserController(IIdentityManager identityManager)
	{
		_identityManager = identityManager;
	}

	// POST: User/Register
	[HttpPost]
	public async Task<ActionResult<Response<JwtToken>>> RegisterUser([FromBody] UserRegisterModel userForRegistration)
	{
		var result = await _identityManager.Create(userForRegistration);

		if (result.IsSuccess)
		{
			//Todo: create a token model with properties like expiry date
			return Ok(new Response<JwtToken>(result.Token));
		}

		foreach (var error in result.Errors)
		{
			ModelState.AddModelError(error.Code, error.Description);
		}

		return BadRequest(ModelState);
	}

	[HttpPost("Login")]
	public async Task<IActionResult> Login([FromBody] UserLogInModel userLogInModel)
	{
		var result = await _identityManager.LogIn(userLogInModel);

		if (!result.IsSuccess)
		{
			return Unauthorized(new Response<AuthenticationResult>(result));
		}

		return Ok(new Response<JwtToken>(result.Token));
	}
}