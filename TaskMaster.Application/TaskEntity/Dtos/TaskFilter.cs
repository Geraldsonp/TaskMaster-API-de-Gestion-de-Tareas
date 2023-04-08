using Issues.Manager.Domain.Enums;

namespace TaskMaster.Application.TaskEntity.Dtos;

public class TaskFilter
{
	public Priority? Priority { get; set; }
	public TicketType? TicketType { get; set; }
}