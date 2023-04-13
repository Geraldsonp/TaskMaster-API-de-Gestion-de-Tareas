using System.Net;
using System.Text.Json;

namespace TaskMaster.Api.Contracts.Responses;

public class ResponseHandler : HttpMessageHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await SendAsync(request, cancellationToken);

        var httpResponse = new HttpResponseMessage(response.StatusCode);

        var customResponse = new Response()
        {
            Data = await response.Content.ReadAsStringAsync(),
            IsSucess = response.IsSuccessStatusCode
        };

        var content = JsonSerializer.Serialize(customResponse);

        httpResponse.Content = new StringContent(content);

        return httpResponse;
    }
}