using TaskMaster.Domain.Enums;

namespace TaskMaster.Application.WorkItemFeature.Dtos;

public class WorkItemFilter
{
	public Priority? Priority { get; set; }
	public WorkItemType? TicketType { get; set; }
}