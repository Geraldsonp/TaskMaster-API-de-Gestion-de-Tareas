using System.ComponentModel.DataAnnotations;
using Issues.Manager.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace TaskMaster.Application.TaskEntity.Dtos;


public class TaskUpdateDto
{
	[SwaggerSchema("The Ticket Title")]
	[Required(ErrorMessage = "Title can not be null.")]
	[MaxLength(50, ErrorMessage = "Max lenght for Title is 50 characters")]
	public string? Title { get; init; }


	[Required(ErrorMessage = "Title can not be null.")]
	[MaxLength(300, ErrorMessage = "Max lenght for description is 100 characters")]
	public string? Description { get; set; }


	[Required, Range(0, 3, ErrorMessage = "Ticket priority does not exist")]
	public Priority? Priority { get; set; }


	[Required, Range(0, 3, ErrorMessage = "Ticket type does not exist")]
	public TicketType? TicketType { get; set; }
}