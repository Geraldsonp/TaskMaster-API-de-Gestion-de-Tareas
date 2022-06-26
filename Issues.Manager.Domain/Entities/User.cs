using Microsoft.AspNetCore.Identity;

namespace Issues.Manager.Domain.Entities;

public class User : BaseEntity
{
    public string IdentityId { get; set; }
    public IdentityUser IdentityUser { get; set; }
    public string FullName { get; set; }
    public ICollection<Issue>? IssuesCreated { get; set; }
}