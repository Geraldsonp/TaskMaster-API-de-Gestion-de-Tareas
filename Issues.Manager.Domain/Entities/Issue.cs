using Issues.Manager.Domain.Enums;

namespace Issues.Manager.Domain.Entities;

public class Issue
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public IssueType IssueType { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Completed { get; set; }
}