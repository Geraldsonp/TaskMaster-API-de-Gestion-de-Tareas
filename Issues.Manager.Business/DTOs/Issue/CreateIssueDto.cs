using System.ComponentModel.DataAnnotations;
using Issues.Manager.Domain.Enums;

namespace Issues.Manager.Business.DTOs;

public class CreateIssueDto
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public Priority Priority { get; set; }
    [Required]
    public IssueType IssueType { get; set; }
}