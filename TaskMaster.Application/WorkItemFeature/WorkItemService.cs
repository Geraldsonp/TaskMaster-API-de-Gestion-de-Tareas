using Mapster;
using TaskMaster.Application.Contracts;
using TaskMaster.Application.ExtensionMethods;
using TaskMaster.Application.WorkItemFeature.Dtos;
using TaskMaster.Domain.Entities;
using TaskMaster.Domain.Exceptions;
using TaskMaster.Domain.ValueObjects;

namespace TaskMaster.Application.WorkItemFeature
{
	public class WorkItemService : IWorkItemService
	{
		private readonly IUnitOfWork _repositoryManager;

		private readonly IAuthenticationStateService UserIdProvider;

		public WorkItemService(
			IUnitOfWork repositoryManager,
			IAuthenticationStateService userIdProvider)
		{
			this.UserIdProvider = userIdProvider;
			this._repositoryManager = repositoryManager;
		
		}

		public WorkItemDto Create(WorkItemCreateDto workItemCreateRequest)
		{
			var userId = UserIdProvider.GetCurrentUserId();


			var issueToSave = workItemCreateRequest.Adapt<Domain.Entities.WorkItem>();
		
			issueToSave.UserId = userId;
		
			_repositoryManager.TaskRepository.Create(issueToSave);
		
			_repositoryManager.SaveChanges();

			return issueToSave.Adapt<WorkItemDto>();
		}

		public WorkItemDto GetById(int id)
		{
			var issue = _repositoryManager.TaskRepository.FindByCondition(i => i.Id == id);
			if (issue is null)
			{
				throw new NotFoundException(nameof(WorkItem), id);
			}
			return issue.Adapt<WorkItemDto>();
		}


		public PagedResponse<WorkItemDto> GetAll(WorkItemFilter ticketFilters, Pagination paging)
		{
			var issues = _repositoryManager.TaskRepository.FindAll();

			if (ticketFilters.WorkItemType is not null)
			{
				issues =
					issues.Where(ticket =>
						ticket.WorkItemType == ticketFilters.WorkItemType);

			}

			if (ticketFilters.Priority is not null)
			{
				issues =
					issues.Where(ticket =>
						ticket.Priority == ticketFilters.Priority);
			}

			var response = issues.ToMappedPagedResponse<Domain.Entities.WorkItem, WorkItemDto>(paging);

			return response;
		}

		public void Update(int id, WorkItemUpdateDto updateRequest)
		{
			var ticket = _repositoryManager.TaskRepository.FindByCondition(ticket => ticket.Id == id);

			if (ticket is null)
			{
				throw new NotFoundException(nameof(WorkItem), id);
			}

			updateRequest.Adapt(ticket);
			
			_repositoryManager.TaskRepository.Update(ticket);

			_repositoryManager.SaveChanges();
		}

		public void Delete(int id)
		{
			var issueToDelete = _repositoryManager.TaskRepository
				.FindByCondition(i => i.Id == id);

			if (issueToDelete is null)
			{
				throw new NotFoundException(nameof(WorkItem), id);
			}

			_repositoryManager.TaskRepository.Delete(issueToDelete);
			_repositoryManager.SaveChanges();
		}

	}
}