namespace Issues.Manager.Domain.Entities;

public class User : BaseEntity
{
    public string IdentityId { get; set; }
    public string FullName { get; set; }
    public ICollection<Issue>? IssuesCreated { get; set; }
    
}