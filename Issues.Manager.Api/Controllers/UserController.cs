using Issues.Manager.Business.Abstractions.LoggerContract;
using Issues.Manager.Business.DTOs;
using Issues.Manager.Business.Services.Account;
using Microsoft.AspNetCore.Mvc;

namespace Issues.Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccountManager _accountManager;
        private readonly ILoggerManager _loggerManager;

        public UserController(IAccountManager accountManager,
            ILoggerManager loggerManager)
        {
            _accountManager = accountManager;
            _loggerManager = loggerManager;
        }
        
        
        // POST: User/Register
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto
            userForRegistration)
        {
            var result = await _accountManager.CreateUser(userForRegistration);
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
        public async Task<IActionResult> Login([FromBody] UserLogInDto userLogInDto)
        {
            if (!await _accountManager.ValidateUser(userLogInDto))
            {
                _loggerManager.LogWarn($"{nameof(Login)}: Authentication Failed. Wrong Username or password");
                return Unauthorized();
            }

            return Ok(new { Token = await _accountManager.CreateToken() });

        }
    
    
    }
    
    
}
