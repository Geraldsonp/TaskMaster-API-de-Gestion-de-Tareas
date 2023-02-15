using System.ComponentModel.DataAnnotations;
using Issues.Manager.Domain.Enums;

namespace Issues.Manager.Application.Models.Issue;

public class TicketCreateRequest
{
    [Required(ErrorMessage = "Title can not be null.")]
    [MaxLength(50, ErrorMessage = "Max lenght for name is 50 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Title can not be null.")]
    [MaxLength(100, ErrorMessage = "Max lenght for name is 50 characters")]
    public string Description { get; set; }

    [Required, Range(0,3, ErrorMessage = "Ticket priority does not exist")]
    public Priority Priority { get; set; }

    [Required, Range(0,3, ErrorMessage = "Ticket type does not exist")]
    public TicketType TicketType { get; set; }
}