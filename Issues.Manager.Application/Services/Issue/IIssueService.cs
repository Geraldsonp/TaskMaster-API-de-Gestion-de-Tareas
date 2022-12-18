using Issues.Manager.Application.DTOs;

namespace Issues.Manager.Application.Services;

public interface IIssueService
{
    IssueReponse Create(CreateIssueRequest issueRequest);
    IssueReponse GetById(int id , bool trackChanges = false);
    IEnumerable<IssueReponse> GetAll(bool trackChanges = false);
    IssueReponse Update(IssueReponse issueReponse);
    void Delete(int id);
}