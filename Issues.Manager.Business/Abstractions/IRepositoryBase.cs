using Issues.Manager.Domain.Entities;
namespace Issues.Manager.Business.Abstractions;

public interface IRepositoryBase
{
    Issue Create(Issue issue);
    int Delete(Issue issue);
    Issue Update(Issue issue);
    Issue GetById(Issue issue);
}