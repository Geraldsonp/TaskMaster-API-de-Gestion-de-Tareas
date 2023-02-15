using AutoMapper;
using Issues.Manager.Application.Contracts;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.Interfaces;
using Issues.Manager.Application.Models.Issue;
using Issues.Manager.Domain.Entities;
using Issues.Manager.Domain.Exceptions;

namespace Issues.Manager.Application.Services.Issue;

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

    public TicketDetailsModel Create(TicketCreateRequest ticketCreateRequest)
    {
        var userId = UserIdProvider.GetCurrentUserId();


        var issueToSave = _mapper.Map<Ticket>(ticketCreateRequest);
        issueToSave.UserId = userId;
        _repositoryManager.TaskRepository.Create(issueToSave);
        _repositoryManager.SaveChanges();
        return _mapper.Map<TicketDetailsModel>(issueToSave);
    }

    public TicketDetailsModel GetById(int id, bool trackChanges = false)
    {
        var issue = _repositoryManager.TaskRepository.FindByCondition(i => i.Id == id, trackChanges);
        if (issue is null)
        {
            throw new IssueNotFoundException(id);
        }
        return _mapper.Map<TicketDetailsModel>(issue);
    }


    public IEnumerable<TicketDetailsModel> GetAll(TicketFilters ticketFilters, bool trackChanges = false)
    {
        var issues = _repositoryManager.TaskRepository.FindAll();
        
        if (ticketFilters.TicketType is not null)
        {
            issues =
                issues.Where(ticket =>
                    ticket.TicketType == ticketFilters.TicketType);
        }

        if (ticketFilters.Priority is not null)
        {
            issues =
                issues.Where(ticket =>
                    ticket.Priority == ticketFilters.Priority);
        }

        var issuesDtos = _mapper.Map<IEnumerable<TicketDetailsModel>>(issues.ToList());

        return issuesDtos;
    }

    public TicketDetailsModel Update(TicketDetailsModel ticketDetailsModel)
    {
        var updatedIssue = _mapper.Map<Ticket>(ticketDetailsModel);
        _repositoryManager.TaskRepository.Update(updatedIssue);
        _repositoryManager.SaveChanges();
        return ticketDetailsModel;
    }

    public void Delete(int id)
    {
        var issueToDelete = _repositoryManager.TaskRepository
            .FindByCondition(i => i.Id == id);

        if (issueToDelete is null)
        {
            throw new IssueNotFoundException(id);
        }
        
        _repositoryManager.TaskRepository.Delete(issueToDelete);
        _repositoryManager.SaveChanges();
    }

}