using Issues.Manager.Domain.Enums;

namespace Issues.Manager.Domain.Entities;

public class Issue : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Priority Priority { get; set; }
    public IssueType IssueType { get; set; }
    public DateTime Created { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int UserId { get; set; }
    
    private bool isCompleted;

    public bool IsCompleted
    {
        get => isCompleted;

        set
        {
            isCompleted = value;
            CompletedAt = DateTime.Now;
        }
    }

}