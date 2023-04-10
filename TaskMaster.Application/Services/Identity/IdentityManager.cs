using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using TaskMaster.Application.Contracts;
using TaskMaster.Application.Interfaces;
using TaskMaster.Application.Models.User;

namespace TaskMaster.Application.Services.Identity;

public class IdentityManager : IIdentityManager
{
	private readonly IMapper _mapper;
	private readonly ITokenManager _tokenManager;
	private readonly UserManager<IdentityUser> _userManager;
	private IdentityUser? _user;

	public IdentityManager(
		IMapper mapper,
		ITokenManager tokenManager,
		IUnitOfWork repositoryManager,
		UserManager<IdentityUser> userManager,
		IConfiguration configuration)
	{
		_mapper = mapper;
		_tokenManager = tokenManager;
		_userManager = userManager;
	}

	public async Task<AuthenticationResult> Create(UserRegisterModel userRegisterModel)
	{
		var user = _mapper.Map<IdentityUser>(userRegisterModel);

		var result = await _userManager.CreateAsync(user, userRegisterModel.Password);

		if (!result.Succeeded)
		{
			return new AuthenticationResult(result.Errors);
		}


		var token = await _tokenManager.GenerateToken(user);

		return new AuthenticationResult(token);
	}

	public async Task<AuthenticationResult> LogIn(UserLogInModel userForAuth)
	{
		_user = await _userManager.FindByNameAsync(userForAuth.UserName);

		var isSuccess = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuth.Password));

		if (!isSuccess)
		{
			return new AuthenticationResult(isSuccess, "UserName Or Password Error");
		}

		var token = await _tokenManager.GenerateToken(_user);

		return new AuthenticationResult(token);
	}
}