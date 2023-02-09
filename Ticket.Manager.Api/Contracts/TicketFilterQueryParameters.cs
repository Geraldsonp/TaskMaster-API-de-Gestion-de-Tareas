using Issues.Manager.Domain.Enums;

namespace Issues.Manager.Api.Contracts;

public class TicketFilterQueryParameters
{
    public Priority? Priority { get; set; }
    public TicketType? TicketType { get; set; }
}