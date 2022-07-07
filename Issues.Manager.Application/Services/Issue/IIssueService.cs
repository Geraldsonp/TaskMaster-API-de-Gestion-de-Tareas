using Issues.Manager.Application.DTOs;

namespace Issues.Manager.Application.Services;

public interface IIssueService
{
    IssueDto Create(CreateIssueDto issueDto, string userId);
    IssueDto GetById(int id , bool trackChanges = false);
    IEnumerable<IssueDto> GetAll(bool trackChanges = false);
    IssueDto Update(IssueDto issueDto);
    void Delete(int id);
}