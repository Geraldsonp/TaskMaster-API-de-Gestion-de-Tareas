namespace TaskMaster.Api.Contracts;

public class TicketFilterQuery
{
	public string? Priority { get; set; }
	public string? TicketType { get; set; }
}