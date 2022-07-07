using Issues.Manager.Domain.Enums;

namespace Issues.Manager.Application.DTOs;

public class IssueDto
{
    public int Id { get; init; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public IssueType IssueType { get; set; }
    public DateTime Created { get; init; }
    public bool IsCompleted { get; set; }
}