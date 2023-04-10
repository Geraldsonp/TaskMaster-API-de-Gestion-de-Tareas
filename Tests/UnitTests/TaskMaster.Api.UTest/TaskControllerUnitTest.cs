using System.Net;
using System.Reflection;
using AutoMapper;
using Moq;
using TaskMaster.Api.Contracts;
using TaskMaster.Api.Controllers;
using TaskMaster.Application.MappingConfigs;
using TaskMaster.Application.TaskEntity;
using TaskMaster.Application.TaskEntity.Dtos;
using TaskMaster.Domain.Enums;
using TaskMaster.Domain.ValueObjects;

namespace TaskMaster.Api.UTests;

public class TaskControllerUnitTest
{
    private Mock<ITaskEntityService> _taskService;
    private TaskController? _taskController;
    private IMapper _mapper;

    private static IEnumerable<Object[]> goodData = new[]
    {
	    new object[]
	    {
		    new TaskCreateDto()
		    {
			    Description = "Test",
			    Title = "Test",
			    Priority = Priority.High,
			    TicketType = TicketType.Bug
		    },
		    HttpStatusCode.OK
	    }
    };

    public TaskControllerUnitTest()
    {
	    var mappingConfig = new MapperConfiguration(x => x.AddMaps(Assembly.GetAssembly(typeof(MappingProfiles))));
		_mapper = new Mapper(mappingConfig);
		_taskService = new Mock<ITaskEntityService>();
    }


	[Fact]
	public void GetTask_ShouldReturn_ListOfTask()
	{
		var mappingConfig = new MapperConfiguration(x => x.AddMaps(Assembly.GetAssembly(typeof(TaskController))));
		_mapper = new Mapper(mappingConfig);
		_taskService.Setup(service => service.GetAll(It.IsAny<TaskFilter>(), It.IsAny<Paggination>()))
			.Returns(new PagedResponse<TaskEntityDto>());
		
		_taskController = new TaskController(_taskService.Object, _mapper);
	}
}