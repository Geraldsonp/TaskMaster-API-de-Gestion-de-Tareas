using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Issues.Manager.Application.DTOs;

namespace TicketManager.Test;

public class Utils
{
    private readonly HttpClient _httpClient;
    public Utils(TestStartUp<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }

}