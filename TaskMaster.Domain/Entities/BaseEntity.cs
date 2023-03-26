using System.ComponentModel.DataAnnotations;

namespace Issues.Manager.Domain.Entities;

public class BaseEntity
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }
}