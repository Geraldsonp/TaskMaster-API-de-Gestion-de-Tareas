using System.Reflection;
using FluentAssertions;
using Moq;
using TaskMaster.Api.Controllers;
using TaskMaster.Application.TaskEntity;
using TaskMaster.Application.TaskEntity.Dtos;
using TaskMaster.Domain.Enums;
using TaskMaster.Domain.ValueObjects;

namespace TaskMaster.Api.UTests;

public class TaskControllerUnitTest
{
    private Mock<ITaskEntityService> _taskService;
    private TaskController? _taskController;
    public static IEnumerable<Object[]> goodData = new[]
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
		    new TaskEntityDto()
		    {
			    Id = 1,
			    Description = "Test",
			    Title = "Test",
			    Priority = Priority.High,
			    TicketType = TicketType.Bug
		    },
		    
	    }
    };

    public TaskControllerUnitTest()
    {
	    _taskService = new Mock<ITaskEntityService>();
    }
    
    [Theory]
    [MemberData(nameof(goodData))]
    public void CreateTask_ShouldReturn_CorrectStatusCode(TaskCreateDto actual, TaskEntityDto expected)
    {
	    _taskService.Setup(service => service.Create(It.IsAny<TaskCreateDto>())).Returns(expected);
		
	    _taskController = new TaskController(_taskService.Object);
	    
	    //Act
	    var result = _taskController.Post(actual);

	    result.Value.Should().Be(actual);
    }


	[Theory]
	[MemberData(nameof(goodData))]
	public void GetTask_ShouldReturn_ListOfTask()
	{
		
		
		_taskService.Setup(service => service.GetAll(It.IsAny<TaskFilter>(), It.IsAny<Paggination>()))
			.Returns(new PagedResponse<TaskEntityDto>());
		
		_taskController = new TaskController(_taskService.Object);
	}
}