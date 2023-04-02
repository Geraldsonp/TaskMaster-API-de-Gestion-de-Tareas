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

	private readonly IAuthenticationStateService UserIdProvider;

	public IssueService(
		IRepositoryManager repositoryManager,
		IMapper mapper, IAuthenticationStateService userIdProvider)
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

	public TicketDetailsModel GetById(int id)
	{
		var issue = _repositoryManager.TaskRepository.FindByCondition(i => i.Id == id);
		if (issue is null)
		{
			throw new IssueNotFoundException(id);
		}
		return _mapper.Map<TicketDetailsModel>(issue);
	}


	public IEnumerable<TicketDetailsModel> GetAll(TicketFilters ticketFilters, PaggingOptions paggingOptions)
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

	public void Update(int id, TicketUpdateRequest updateRequest)
	{
		var ticket = _repositoryManager.TaskRepository.FindByCondition(ticket => ticket.Id == id);

		if (ticket is null)
		{
			throw new IssueNotFoundException(id);
		}

		if (updateRequest.TicketType is not null)
			ticket.TicketType = updateRequest.TicketType.Value;

		if (updateRequest.Priority is not null)
			ticket.Priority = updateRequest.Priority.Value;

		if (updateRequest.Description is not null)
			ticket.Description = updateRequest.Description;

		if (updateRequest.Title is not null)
			ticket.Title = updateRequest.Title;

		_repositoryManager.TaskRepository.Update(ticket);

		_repositoryManager.SaveChanges();
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