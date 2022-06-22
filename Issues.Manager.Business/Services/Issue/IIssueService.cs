using Issues.Manager.Business.DTOs;
using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Business.Services;

public interface IIssueService
{
    IssueDto Create(CreateIssueDto issueDto);
    IssueDto GetById(int id);
    IEnumerable<IssueDto> GetAll();
    IssueDto Update(IssueDto issueDto);
    bool Delete(int id);
}