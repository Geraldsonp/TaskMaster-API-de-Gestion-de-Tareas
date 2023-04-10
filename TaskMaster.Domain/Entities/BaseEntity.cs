using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Domain.Entities;

public class BaseEntity
{
	public int Id { get; set; }

	[Required]
	public string UserId { get; set; }
}