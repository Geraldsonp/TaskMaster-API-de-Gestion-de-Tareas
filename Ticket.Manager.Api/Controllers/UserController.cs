using Issues.Manager.Api.ActionFilters;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.Services.Identity;
using Issues.Manager.Application.Services.Logger;
using Issues.Manager.Application.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Issues.Manager.Api.Controllers;

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
    [ServiceFilter(typeof(IsModelValidFilterAttribute))]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequest userForRegistration)
    {
        var result = await _identityManager.Create(userForRegistration);

        if (result.IsSuccess)
        {
            return Ok(result.Token);
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return BadRequest(ModelState);
    }

    [HttpPost("Login")]
    [ServiceFilter(typeof(IsModelValidFilterAttribute))]
    public async Task<IActionResult> Login([FromBody] UserLogInRequest userLogInRequest)
    {
        var result = await _identityManager.LogIn(userLogInRequest);

        if (!result.IsSuccess)
        {
            return Unauthorized(result.Error);
        }

        return Ok(result.Token);
    }
}