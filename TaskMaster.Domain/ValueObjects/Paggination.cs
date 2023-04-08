namespace TaskMaster.Domain.ValueObjects
{
	public class Paggination
	{
		public int PageSize { get; set; } = 100;
		public int PageNumber { get; set; } = 1;
	}
}