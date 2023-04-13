using TaskMaster.Application.WorkItemFeature.Dtos;
using TaskMaster.Domain.ValueObjects;

namespace TaskMaster.Application.WorkItemFeature;

public interface IWorkItemService
{
	WorkItemDto Create(WorkItemCreateDto workItemCreateRequest);
	WorkItemDto GetById(int id);
	PagedResponse<WorkItemDto> GetAll(WorkItemFilter workItemFilter, Paggination paggination);
	void Update(int id, WorkItemUpdateDto updateDto);
	void Delete(int id);
}