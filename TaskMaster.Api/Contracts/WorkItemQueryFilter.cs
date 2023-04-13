using Mapster.Utils;
using TaskMaster.Api.CustomAttributes;
using TaskMaster.Domain.Enums;

namespace TaskMaster.Api.Contracts;

public class WorkItemQueryFilter
{
	[AllowedValues(Values = new [] { "Low","Medium","High" })]
	public string? Priority { get; set; }
	
	
	[AllowedValues(Values = new [] { "Bug","Documentation","Feature" })]
	public string? TicketType { get; set; }
}