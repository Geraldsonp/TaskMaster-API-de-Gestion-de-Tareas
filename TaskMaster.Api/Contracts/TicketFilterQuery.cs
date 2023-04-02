using Issues.Manager.Domain.Enums;

namespace Issues.Manager.Api.Contracts;

public class TicketFilterQuery
{
	public string? Priority { get; set; }
	public string? TicketType { get; set; }
}