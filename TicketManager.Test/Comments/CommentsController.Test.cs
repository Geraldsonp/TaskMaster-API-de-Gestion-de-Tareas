using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace TicketManager.Test.Comments;

public class CommentsControllerTest: IClassFixture<TestStartUp<Program>>
{
    private readonly HttpClient _httpClient;

    public CommentsControllerTest(TestStartUp<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task GetComments_ShouldReturn_ListOfComments()
    {
        //Arrange

        //Act
        var result = await _httpClient.GetAsync($"api/Issue/1/Comment");

        //Assert
        result.IsSuccessStatusCode.Should().BeFalse();
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}