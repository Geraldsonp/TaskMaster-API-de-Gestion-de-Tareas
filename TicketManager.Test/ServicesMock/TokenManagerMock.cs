using System.Threading.Tasks;
using Issues.Manager.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace TicketManager.Test.ServicesMock;

public class TokenManagerMock: ITokenManager
{
    public Task<string> GenerateToken(IdentityUser? claims)
    {
        return Task.FromResult("a072501d-c2aa-499e-a0a9-696204de7776");
    }
}