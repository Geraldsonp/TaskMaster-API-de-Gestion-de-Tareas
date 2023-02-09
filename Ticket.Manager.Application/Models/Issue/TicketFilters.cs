using Issues.Manager.Domain.Enums;

namespace Issues.Manager.Application.DTOs;

public class TicketFilters
{
    public Priority? Priority { get; set; }
    public TicketType? TicketType { get; set; }
}