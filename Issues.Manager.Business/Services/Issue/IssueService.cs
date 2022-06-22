using AutoMapper;
using Issues.Manager.Business.Abstractions.RepositoryContracts;
using Issues.Manager.Business.DTOs;
using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Business.Services;

public class IssueService : IIssueService
{
    private readonly IRepositoryBase<Issue> _issueRepository;
    private readonly IMapper _mapper;

    public IssueService(IRepositoryBase<Issue> issueRepository, IMapper mapper)
    {
        _issueRepository = issueRepository;
        _mapper = mapper;
    }
    public IssueDto Create(CreateIssueDto issueDto)
    {
        var mappedIssue = _mapper.Map<Issue>(issueDto);
        mappedIssue.Created = DateTime.Now;
        var entity = _issueRepository.Create(mappedIssue);
        return _mapper.Map<IssueDto>(entity);
    }

    public IssueDto GetById(int id)
    {
        var issue = _issueRepository.GetById(id);
        if (issue is null)
        {
            throw new NullReferenceException($"The Issue with id {id} Does not exist in the database");
        }

        return _mapper.Map<IssueDto>(issue);
    }

    public IEnumerable<IssueDto> GetAll()
    {
        var issues = _issueRepository.GetAll();
        var mappedIssues = _mapper.Map<IEnumerable<IssueDto>>(issues);
        return mappedIssues;
    }

    public IssueDto Update(IssueDto issueDto)
    {
        var mappedIssue = _mapper.Map<Issue>(issueDto);
        _ = _issueRepository.Update(mappedIssue);
        return issueDto;
    }

    public bool Delete(int id)
    {
        var result =  _issueRepository.Delete(id);
        if (result == 0)
        {
            return false;
        }

        return true;
    }
}