namespace Issues.Manager.Domain.Entities;

public class User : BaseEntity
{
    public new string Id { get; set; }
    public string FullName { get; set; }

    public ICollection<Ticket>? IssuesCreated { get; set; }
}