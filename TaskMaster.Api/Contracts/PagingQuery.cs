namespace Issues.Manager.Api.Contracts;

public class PagingQuery
{
	public int PageSize { get; set; } = 100;
	public int PageNumber { get; set; } = 1;
}