using System.Security.Claims;
using Issues.Manager.Api.ActionFilters;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.Services.Identity;
using Issues.Manager.Application.Services.Logger;
using Issues.Manager.Application.Services.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Issues.Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IIdentityManager _identityManager;
        private readonly ILoggerManager _loggerManager;
        private readonly ITokenManager _tokenManager;

        public UserController(IIdentityManager identityManager,
            ILoggerManager loggerManager,
            ITokenManager tokenManager)
        {
            _identityManager = identityManager;
            _loggerManager = loggerManager;
            _tokenManager = tokenManager;

        }
        
        
        // POST: User/Register
        [HttpPost]
        [ServiceFilter(typeof(IsModelValidFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequest
            userForRegistration)
        {
            var result = await _identityManager.Create(userForRegistration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }

        [HttpPost("Login")]
        [ServiceFilter(typeof(IsModelValidFilterAttribute))]
        public async Task<IActionResult> Login([FromBody] UserLogInRequest userLogInRequest)
        {
            var Result = await _identityManager.ValidateUser(userLogInRequest);
            if (!Result.Item1)
            {
                _loggerManager.LogWarn($"{nameof(Login)}: Authentication Failed. Wrong Username or password");
                return Unauthorized();
            }
            var claims = new List<Claim>
            {
               
                new Claim(ClaimTypes.Name, Result.Item2.UserName),
                new Claim(ClaimTypes.NameIdentifier, Result.Item2.Id)
            };

            return Ok(new { Token = await _tokenManager.GenerateToken(claims) });

        }
    
    
    }
    
    
}
