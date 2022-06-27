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
    private readonly IUserRepository _userRepository;

    public IssueService(IRepositoryBase<Issue> issueRepository, 
        IMapper mapper, 
        ILoggerManager loggerManager, 
        IUserRepository userRepository)
    {
        _issueRepository = issueRepository;
        _mapper = mapper;
        _loggerManager = loggerManager;
        _userRepository = userRepository;
    }
    public IssueDto Create(CreateIssueDto issueDto, string identityId)
    {
        var issueToSave = _mapper.Map<Issue>(issueDto);
        issueToSave.Created = DateTime.Now;
        issueToSave.UserId = GetUser(identityId).Id;
        var IssueSaved = _issueRepository.Create(issueToSave);
        return _mapper.Map<IssueDto>(IssueSaved);
    }

    public IssueDto GetById(int id , string identityId)
    {
        var user = GetUser(identityId);
        var issue = _issueRepository.FindByConditionAsync(i => i.Id == id && i.UserId == user.Id);
        if (issue is null)
        {
            _loggerManager.LogError($"Issue With ID: {id} Does not Exist");
            throw new NullReferenceException($"The Issue with id {id} Does not exist in the database");
            
        }
        _loggerManager.LogError($"Fetched issue Id: {id} successfully");
        return _mapper.Map<IssueDto>(issue);
    }

    public IEnumerable<IssueDto> GetAll( string identityId)
    {
        //todo: Implement Pagination and sorting
        var user = GetUser(identityId);
        var issues = _issueRepository.GetAll(user.Id);
        var mappedIssues = _mapper.Map<IEnumerable<IssueDto>>(issues);
        return mappedIssues;
    }

    public IssueDto Update(IssueDto issueDto)
    {
        var mappedIssue = _mapper.Map<Issue>(issueDto);
        _ = _issueRepository.Update(mappedIssue);
        return issueDto;
    }

    public bool Delete(int id , string identityId)
    {
        var user = GetUser(identityId);
        var issueToDelete = _issueRepository.FindByConditionAsync(i => i.Id == id && i.UserId == user.Id);
        if (issueToDelete is null)
        {
            _loggerManager.LogError($"Issue With Id {id} Does not exist");
            return false;
        }

        _issueRepository.Delete(issueToDelete);

        return true;
    }
    //todo: Implement Marking Complete

    private User GetUser(string identityId)
    {
        var user = _userRepository.GetById(identityId);
        if (user is null)
        {
            throw new NullReferenceException("Unable to get user with IdentityId");
        }
        _loggerManager.LogDebug($"Get user returned the user {user.FullName} with id {user.Id}");
        return user;
    }
}