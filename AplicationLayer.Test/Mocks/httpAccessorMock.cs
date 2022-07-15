using Issues.Manager.Application.Services.HttpContextAccessor;

namespace AplicationLayer.Test.Mocks;

public class httpAccessorMock : IHttpAccessor
{

    public int GetCurrentIdentityId()
    {
       
         
            return 1;
    }
}