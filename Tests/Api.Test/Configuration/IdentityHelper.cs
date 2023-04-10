using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TaskMaster.Application.Models.User;

namespace TicketManager.Test.Configuration;

public class IdentityHelper
{
	private readonly HttpClient _httpClient;

	public IdentityHelper(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}
	public async Task<string> AuthenticateUser()
	{
		var loginRequest = new UserLogInModel()
		{
			Password = "!TestAll123",
			UserName = "Tester300"
		};
		var response = await _httpClient.PostAsJsonAsync("api/user/login", loginRequest);
		if (response.IsSuccessStatusCode)
		{
			var token = await response.Content.ReadAsStringAsync();
			return token;
		}
		else
		{
			return await CreateTestUser();
		}
	}

	private async Task<string> CreateTestUser()
	{
		var userRequest = new UserRegisterModel()
		{
			Email = "Testing@Email.com",
			FirstName = "Testes",
			LastName = "Test",
			Password = "!TestAll123",
			UserName = "Tester300"
		};
		var response = await _httpClient.PostAsJsonAsync("api/User", userRequest);
		if (response.IsSuccessStatusCode)
		{
			var token = await response.Content.ReadAsStringAsync();
			return token;
		}
		else
		{
			return "";
		}
	}
}