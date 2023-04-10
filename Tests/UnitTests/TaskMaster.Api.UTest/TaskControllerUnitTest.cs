using System.Reflection;
using AutoMapper;
using Moq;
using TaskMaster.Api.Controllers;
using TaskMaster.Application.MappingConfigs;
using TaskMaster.Application.TaskEntity;

namespace TaskMaster.Api.UTests;

public class TaskControllerUnitTest
{
    private Mock<ITaskEntityService> _taskService;
    private TaskController? _taskController;
    private IMapper _mapper;

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
		
		
		_taskController = new TaskController(_taskService.Object, _mapper);
	}
}