using Issues.Manager.Api.ActionFilters;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.Services.Identity;
using Issues.Manager.Application.Services.Logger;
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

        public UserController(IIdentityManager identityManager,
            ILoggerManager loggerManager)
        {
            _identityManager = identityManager;
            _loggerManager = loggerManager;
        }
        
        
        // POST: User/Register
        [HttpPost]
        [ServiceFilter(typeof(IsModelValidFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto
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
        public async Task<IActionResult> Login([FromBody] UserLogInDto userLogInDto)
        {
            if (!await _identityManager.ValidateUser(userLogInDto))
            {
                _loggerManager.LogWarn($"{nameof(Login)}: Authentication Failed. Wrong Username or password");
                return Unauthorized();
            }

            return Ok(new { Token = await _identityManager.CreateToken() });

        }
    
    
    }
    
    
}
