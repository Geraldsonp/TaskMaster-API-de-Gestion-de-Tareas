using TaskMaster.Application.TaskEntity.Dtos;
using TaskMaster.Domain.ValueObjects;

namespace Issues.Manager.Application.Interfaces;

public interface ITaskEntityService
{
	TaskEntityDto Create(TaskCreateDto request);
	TaskEntityDto GetById(int id);
	PagedResponse<TaskEntityDto> GetAll(TaskFilter taskFilter, Paggination paggination);
	void Update(int id, TaskUpdateDto updateDto);
	void Delete(int id);
}