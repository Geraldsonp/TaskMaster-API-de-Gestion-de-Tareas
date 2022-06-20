namespace Issues.Manager.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string IdentityId { get; set; }
    public string FullName { get; set; }
    public ICollection<Issue>? IssuesCreated { get; set; }
    
}