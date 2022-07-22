using System.ComponentModel.DataAnnotations;
using Issues.Manager.Domain.Enums;

namespace Issues.Manager.Domain.Entities;

public class Issue : BaseEntity
{
    [Required(ErrorMessage = "Title can not be null.")]
    [MaxLength(50, ErrorMessage = "Max lenght for name is 50 characters")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "Title can not be null.")]
    [MaxLength(100, ErrorMessage = "Max lenght for name is 50 characters")]
    public string? Description { get; set; }
    [Required, Range(0,3, ErrorMessage = "Issue priority does not exist")]
    public Priority Priority { get; set; }
    [Required, Range(0,3, ErrorMessage = "Issue type does not exist")]
    public IssueType IssueType { get; set; }
    public DateTime Created { get; init; }
    public DateTime? CompletedAt { get; set; }
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
    
    public int UserId { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

}