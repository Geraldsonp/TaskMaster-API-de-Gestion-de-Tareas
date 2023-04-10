using AutoMapper;
using TaskMaster.Application.Contracts;
using TaskMaster.Application.ExtensionMethods;
using TaskMaster.Application.TaskEntity.Dtos;
using TaskMaster.Domain.Exceptions;
using TaskMaster.Domain.ValueObjects;

namespace TaskMaster.Application.TaskEntity;

public class TaskEntityService : ITaskEntityService
{
	private readonly IUnitOfWork _repositoryManager;
	private readonly IMapper _mapper;

	private readonly IAuthenticationStateService UserIdProvider;

	public TaskEntityService(
		IUnitOfWork repositoryManager,
		IMapper mapper, IAuthenticationStateService userIdProvider)
	{
		this.UserIdProvider = userIdProvider;
		this._repositoryManager = repositoryManager;
		_mapper = mapper;
	}

	public TaskEntityDto Create(TaskCreateDto ticketCreateRequest)
	{
		var userId = UserIdProvider.GetCurrentUserId();


		var issueToSave = _mapper.Map<Domain.Entities.TaskEntity>(ticketCreateRequest);
		issueToSave.UserId = userId;
		_repositoryManager.TaskRepository.Create(issueToSave);
		_repositoryManager.SaveChanges();
		return _mapper.Map<TaskEntityDto>(issueToSave);
	}

	public TaskEntityDto GetById(int id)
	{
		var issue = _repositoryManager.TaskRepository.FindByCondition(i => i.Id == id);
		if (issue is null)
		{
			throw new NotFoundException(nameof(TaskEntity), id);
		}
		return _mapper.Map<TaskEntityDto>(issue);
	}


	public PagedResponse<TaskEntityDto> GetAll(TaskFilter ticketFilters, Paggination pagging)
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

		var response = issues.ToMappedPagedResponse<Domain.Entities.TaskEntity, TaskEntityDto>(pagging.PageSize, pagging.PageNumber, _mapper);

		return response;
	}

	public void Update(int id, TaskUpdateDto updateRequest)
	{
		var ticket = _repositoryManager.TaskRepository.FindByCondition(ticket => ticket.Id == id);

		if (ticket is null)
		{
			throw new NotFoundException(nameof(TaskEntity), id);
		}

		_mapper.Map(ticket, updateRequest);

		_repositoryManager.TaskRepository.Update(ticket);

		_repositoryManager.SaveChanges();
	}

	public void Delete(int id)
	{
		var issueToDelete = _repositoryManager.TaskRepository
			.FindByCondition(i => i.Id == id);

		if (issueToDelete is null)
		{
			throw new NotFoundException(nameof(TaskEntity), id);
		}

		_repositoryManager.TaskRepository.Delete(issueToDelete);
		_repositoryManager.SaveChanges();
	}

}