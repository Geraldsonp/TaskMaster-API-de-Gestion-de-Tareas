using AutoMapper;
using Issues.Manager.Application.Abstractions.RepositoryContracts;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.Services.Logger;
using Issues.Manager.Domain.Entities;
using Issues.Manager.Domain.Exceptions;

namespace Issues.Manager.Application.Services;

public class IssueService : IIssueService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly ILoggerManager _loggerManager;


    public IssueService(
        IRepositoryManager _repositoryManager,
        IMapper mapper,
        ILoggerManager loggerManager)
    {
        
        this._repositoryManager = _repositoryManager;
        _mapper = mapper;
        _loggerManager = loggerManager;

    }

    public IssueDto Create(CreateIssueDto createIssueDto, string identityId)
    {
        var userId = _repositoryManager.User
            .FindByCondition(u => 
                    u.IdentityId == identityId,
                false).Id;
        
        var issueToSave = _mapper.Map<Issue>(createIssueDto);
        issueToSave.Created = DateTime.Now;
        issueToSave.UserId = userId;
        _repositoryManager.Issue.Create(issueToSave);
        _repositoryManager.SaveChanges();
        return _mapper.Map<IssueDto>(issueToSave);
    }

    public IssueDto GetById(int id, bool trackChanges = false)
    {
        var issue = _repositoryManager.Issue.FindByCondition(i => i.Id == id, trackChanges);
        if (issue is null)
        {
            throw new IssueNotFoundException(id);
        }
        return _mapper.Map<IssueDto>(issue);
    }
    

    public IEnumerable<IssueDto> GetAll( bool trackChanges = false)
    {
        //todo: Implement Pagination and sorting
        var issues = _repositoryManager.Issue.FindAll(trackChanges).ToList();
        var issuesDtos = _mapper.Map<IEnumerable<IssueDto>>(issues);
        return issuesDtos;
    }

    public IssueDto Update(IssueDto issueDto)
    {
        var mappedIssue = _mapper.Map<Issue>(issueDto);
        _repositoryManager.Issue.Update(mappedIssue);
        _repositoryManager.SaveChanges();
        return issueDto;
    }

    public void Delete(int id)
    {
        var issueToDelete = _repositoryManager.Issue
            .FindByCondition(i => i.Id == id);
        if (issueToDelete is null)
        {
            throw new IssueNotFoundException(id);
        }
        _repositoryManager.Issue.Delete(issueToDelete);
        _repositoryManager.SaveChanges();
    }
    //todo: Implement Marking Complete
    
}