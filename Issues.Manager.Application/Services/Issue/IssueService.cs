using AutoMapper;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Domain.Contracts;
using Issues.Manager.Domain.Entities;
using Issues.Manager.Domain.Exceptions;

namespace Issues.Manager.Application.Services;

public class IssueService : IIssueService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    private readonly IUserIdProvider UserIdProvider;

    public IssueService(
        IRepositoryManager repositoryManager,
        IMapper mapper, IUserIdProvider userIdProvider)
    {
        this.UserIdProvider = userIdProvider;
        this._repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public IssueReponse Create(CreateIssueRequest createIssueRequest)
    {
        var identityID = UserIdProvider.GetCurrentUserId();
        var userId = _repositoryManager.UsersRepository
            .FindByCondition(u =>
                    u.IdentityId == identityID).Id;

        var issueToSave = _mapper.Map<Issue>(createIssueRequest);
        issueToSave.UserId = userId;
        _repositoryManager.IssuesRepository.Create(issueToSave);
        _repositoryManager.SaveChanges();
        return _mapper.Map<IssueReponse>(issueToSave);
    }

    public IssueReponse GetById(int id, bool trackChanges = false)
    {
        var issue = _repositoryManager.IssuesRepository.FindByCondition(i => i.Id == id, trackChanges);
        if (issue is null)
        {
            throw new IssueNotFoundException(id);
        }
        return _mapper.Map<IssueReponse>(issue);
    }


    public IEnumerable<IssueReponse> GetAll(bool trackChanges = false)
    {
        var issues = _repositoryManager.IssuesRepository.FindAll(trackChanges).ToList();
        var issuesDtos = _mapper.Map<IEnumerable<IssueReponse>>(issues);
        return issuesDtos;
    }

    public IssueReponse Update(IssueReponse issueReponse)
    {
        var updatedIssue = _mapper.Map<Issue>(issueReponse);
        _repositoryManager.IssuesRepository.Update(updatedIssue);
        _repositoryManager.SaveChanges();
        return issueReponse;
    }

    public void Delete(int id)
    {
        var issueToDelete = _repositoryManager.IssuesRepository
            .FindByCondition(i => i.Id == id);

        if (issueToDelete is null)
        {
            throw new IssueNotFoundException(id);
        }
        
        _repositoryManager.IssuesRepository.Delete(issueToDelete);
        _repositoryManager.SaveChanges();
    }

}