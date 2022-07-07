namespace Issues.Manager.Domain.Exceptions;

public class IssueNotFoundException : Exception
{

    public IssueNotFoundException()
    {
        
    }
    public IssueNotFoundException(int issueId)
        : base($"Issue with the Id: {issueId} Can not be found") { }
}