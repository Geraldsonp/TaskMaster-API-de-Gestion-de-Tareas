using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Issues.Manager.Application.DTOs;
using Xunit;

namespace TicketManager.Test.Comments;

public class CommentsControllerTest: IClassFixture<TestStartUp<Program>>
{
    private readonly HttpClient _httpClient;

    public CommentsControllerTest(TestStartUp<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }
    private async Task GenerateToken()
    {
        var userRequest = new UserRegisterRequest()
        {
            Email = "Testing@Email.com",
            FirstName = "Testes",
            LastName = "Test",
            Password = "!TestAll123",
            UserName = "Tester300"
        };

        var response = await _httpClient.PostAsJsonAsync("api/User", userRequest);

        var token = await response.Content.ReadAsStringAsync();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    [Fact]
    public async Task GetComments_ShouldReturn_ListOfComments()
    {
        //Arrange
         await GenerateToken();
        //Act
        var result = await _httpClient.GetAsync($"api/Issue/1/Comment");

        //Assert
        result.IsSuccessStatusCode.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}