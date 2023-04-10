namespace TaskMaster.Application.Contracts
{
	public interface IAuthenticationStateService
	{
		string GetCurrentUserId();
	}
}