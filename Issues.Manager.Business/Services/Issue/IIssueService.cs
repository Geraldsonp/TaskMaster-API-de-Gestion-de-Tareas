using Issues.Manager.Business.DTOs;
using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Business.Services;

public interface IIssueService
{
    IssueDto Create(CreateIssueDto issueDto, string userId);
    IssueDto GetById(int id , string identityId);
    IEnumerable<IssueDto> GetAll(string userId);
    IssueDto Update(IssueDto issueDto);
    bool Delete(int id, string userId);
}