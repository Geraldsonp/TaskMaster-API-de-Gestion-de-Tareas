using AutoMapper;
using Issues.Manager.Application.Abstractions.RepositoryContracts;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Domain.Entities;
using Issues.Manager.Domain.Exceptions;

namespace Issues.Manager.Application.Services;

public class IssueService : IIssueService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;


    public IssueService(
        IRepositoryManager repositoryManager,
        IMapper mapper)
    {
        this._repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public IssueReponse Create(CreateIssueRequest createIssueRequest, string identityId)
    {
        var userId = _repositoryManager.User
            .FindByCondition(u => 
                    u.IdentityId == identityId).Id;
        
        var issueToSave = _mapper.Map<Issue>(createIssueRequest);
        issueToSave.UserId = userId;
        _repositoryManager.Issue.Create(issueToSave);
        _repositoryManager.SaveChanges();
        return _mapper.Map<IssueReponse>(issueToSave);
    }

    public IssueReponse GetById(int id, bool trackChanges = false)
    {
        var issue = _repositoryManager.Issue.FindByCondition(i => i.Id == id, trackChanges);
        if (issue is null)
        {
            throw new IssueNotFoundException(id);
        }
        return _mapper.Map<IssueReponse>(issue);
    }
    

    public IEnumerable<IssueReponse> GetAll( bool trackChanges = false)
    {
        var issues = _repositoryManager.Issue.FindAll(trackChanges).ToList();
        var issuesDtos = _mapper.Map<IEnumerable<IssueReponse>>(issues);
        return issuesDtos;
    }

    public IssueReponse Update(IssueReponse issueReponse)
    {
        var mappedIssue = _mapper.Map<Issue>(issueReponse);
        _repositoryManager.Issue.Update(mappedIssue);
        _repositoryManager.SaveChanges();
        return issueReponse;
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

}