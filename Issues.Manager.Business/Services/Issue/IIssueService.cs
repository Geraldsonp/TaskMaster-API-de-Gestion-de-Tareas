using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Business.Services;

public interface IIssueService
{
    Issue Create(Issue issue);
    Issue GetById(int id);
    IEnumerable<Issue> GetAll();
    Issue Update(Issue issue);
    bool Delete(int id);
}