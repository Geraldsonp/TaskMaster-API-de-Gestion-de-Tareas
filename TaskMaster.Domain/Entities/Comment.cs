namespace TaskMaster.Domain.Entities;

public class Comment : BaseEntity
{
	public string? Content { get; set; }
	public DateTime PostedDate { get; set; } = DateTime.Now;
	public WorkItem Ticket { get; set; }

}