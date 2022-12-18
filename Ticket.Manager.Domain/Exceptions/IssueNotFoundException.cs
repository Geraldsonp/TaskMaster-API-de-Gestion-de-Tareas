namespace Issues.Manager.Domain.Exceptions;

public class IssueNotFoundException : Exception
{

    public IssueNotFoundException(int issueId)
        : base($"Ticket with the Id: {issueId} Can not be found") { }
}