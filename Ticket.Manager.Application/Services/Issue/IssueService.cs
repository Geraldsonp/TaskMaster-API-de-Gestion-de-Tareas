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

    public TicketDetailsModel Create(CreateIssueRequest createIssueRequest)
    {
        var identityID = UserIdProvider.GetCurrentUserId();
        var userId = _repositoryManager.UsersRepository
            .FindByCondition(u =>
                    u.IdentityId == identityID).Id;

        var issueToSave = _mapper.Map<Ticket>(createIssueRequest);
        issueToSave.UserId = userId;
        _repositoryManager.IssuesRepository.Create(issueToSave);
        _repositoryManager.SaveChanges();
        return _mapper.Map<TicketDetailsModel>(issueToSave);
    }

    public TicketDetailsModel GetById(int id, bool trackChanges = false)
    {
        var issue = _repositoryManager.IssuesRepository.FindByCondition(i => i.Id == id, trackChanges);
        if (issue is null)
        {
            throw new IssueNotFoundException(id);
        }
        return _mapper.Map<TicketDetailsModel>(issue);
    }


    public IEnumerable<TicketDetailsModel> GetAll(TicketFilters ticketFilters, bool trackChanges = false)
    {
        var issues = _repositoryManager.IssuesRepository.FindAll();
        
        if (ticketFilters.TicketType is not null)
        {
            issues =
                _repositoryManager.IssuesRepository.FindRangeByCondition(ticket =>
                    ticket.TicketType == ticketFilters.TicketType);
        }

        if (ticketFilters.Priority is not null)
        {
            issues =
                _repositoryManager.IssuesRepository.FindRangeByCondition(ticket =>
                    ticket.Priority == ticketFilters.Priority);
        }

        var issuesDtos = _mapper.Map<IEnumerable<TicketDetailsModel>>(issues);

        return issuesDtos;
    }

    public TicketDetailsModel Update(TicketDetailsModel ticketDetailsModel)
    {
        var updatedIssue = _mapper.Map<Ticket>(ticketDetailsModel);
        _repositoryManager.IssuesRepository.Update(updatedIssue);
        _repositoryManager.SaveChanges();
        return ticketDetailsModel;
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