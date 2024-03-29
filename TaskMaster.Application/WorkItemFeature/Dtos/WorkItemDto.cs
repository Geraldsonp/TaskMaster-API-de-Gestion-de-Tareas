﻿using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;
using TaskMaster.Domain.Enums;

namespace TaskMaster.Application.WorkItemFeature.Dtos;

[SwaggerSchema(Required = new[] { "Ticket" }, Title = "Ticket")]

public class WorkItemDto
{
	[SwaggerSchema("The Ticket identifier", ReadOnly = true)]
	public int Id { get; set; }

	[SwaggerSchema("The Ticket Title")]
	[Required(ErrorMessage = "Title can not be null.")]
	[MaxLength(50, ErrorMessage = "Max lenght for Title is 50 characters")]
	public string? Title { get; init; }


	[Required(ErrorMessage = "Title can not be null.")]
	[MaxLength(300, ErrorMessage = "Max lenght for description is 100 characters")]
	public string? Description { get; set; }


	[Required, Range(0, 3, ErrorMessage = "Ticket priority does not exist")]
	public Priority Priority { get; set; }


	[Required, Range(0, 3, ErrorMessage = "Ticket type does not exist")]
	public WorkItemType WorkItemType { get; set; }


	[SwaggerSchema("The Ticket Creation Date", ReadOnly = true)]
	public DateTime Created { get; init; }


	[SwaggerSchema("The Ticket Completion Date ", ReadOnly = true)]
	public DateTime? CompletedAt { get; set; }

	public string UserId { get; set; }

	public bool IsCompleted;
}