using System.ComponentModel.DataAnnotations;
using Issues.Manager.Domain.Enums;

namespace Issues.Manager.Application.DTOs;

public class CreateIssueDto
{
    [Required]
    public string? Title { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required, Range(0,3, ErrorMessage = "Issue type does not exist")]
    public Priority Priority { get; set; }
    [Required, Range(0,3, ErrorMessage = "Issue type does not exist")]
    public IssueType IssueType { get; set; }
}