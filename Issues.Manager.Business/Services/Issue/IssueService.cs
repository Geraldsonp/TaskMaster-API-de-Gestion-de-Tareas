using AutoMapper;
using Issues.Manager.Business.Abstractions.LoggerContract;
using Issues.Manager.Business.Abstractions.RepositoryContracts;
using Issues.Manager.Business.DTOs;
using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Business.Services;

public class IssueService : IIssueService
{
    private readonly IRepositoryBase<Issue> _issueRepository;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _loggerManager;

    public IssueService(IRepositoryBase<Issue> issueRepository, IMapper mapper, ILoggerManager loggerManager)
    {
        _issueRepository = issueRepository;
        _mapper = mapper;
        _loggerManager = loggerManager;
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
            _loggerManager.LogError($"Issue With ID: {id} Does not Exist");
            throw new NullReferenceException($"The Issue with id {id} Does not exist in the database");
            
        }
        _loggerManager.LogError($"Fetched issue Id: {id} successfully");
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