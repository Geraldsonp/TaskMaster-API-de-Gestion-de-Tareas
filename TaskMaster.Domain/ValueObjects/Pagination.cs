namespace TaskMaster.Domain.ValueObjects
{
	public class Pagination
	{
		public int PageSize { get; set; } = 100;
		public int PageNumber { get; set; } = 1;
	}
}