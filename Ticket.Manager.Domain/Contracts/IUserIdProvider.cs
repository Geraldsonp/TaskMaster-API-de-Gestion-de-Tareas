namespace Issues.Manager.Domain.Contracts
{
    public interface IUserIdProvider
    {
         string GetCurrentUserId();
    }
}