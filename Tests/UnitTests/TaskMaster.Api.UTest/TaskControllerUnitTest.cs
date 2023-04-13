using FluentAssertions;
using Moq;
using TaskMaster.Api.Controllers;
using TaskMaster.Application.WorkItemFeature;
using TaskMaster.Application.WorkItemFeature.Dtos;
using TaskMaster.Domain.Enums;
using TaskMaster.Domain.ValueObjects;

namespace TaskMaster.Api.UTests;

public class TaskControllerUnitTest
{
    private Mock<IWorkItemService> _taskService;
    private WorkItemController? _taskController;
    public static IEnumerable<Object[]> goodData = new[]
    {
	    new object[]
	    {
		    new WorkItemCreateDto()
		    {
			    Description = "Test",
			    Title = "Test",
			    Priority = Priority.High,
			    WorkItemType = WorkItemType.Bug
		    },
		    new WorkItemDto()
		    {
			    Id = 1,
			    Description = "Test",
			    Title = "Test",
			    Priority = Priority.High,
			    WorkItemType = WorkItemType.Bug
		    },
		    
	    }
    };

    public TaskControllerUnitTest()
    {
	    _taskService = new Mock<IWorkItemService>();
    }
    
    [Theory]
    [MemberData(nameof(goodData))]
    public void CreateTask_ShouldReturn_CorrectStatusCode(WorkItemCreateDto actual, WorkItemDto expected)
    {
	    _taskService.Setup(service => service.Create(It.IsAny<WorkItemCreateDto>())).Returns(expected);
		
	    _taskController = new WorkItemController(_taskService.Object);
	    
	    //Act
	    var result = _taskController.Post(actual);

	    result.Value.Should().Be(actual);
    }


	[Theory]
	[MemberData(nameof(goodData))]
	public void GetTask_ShouldReturn_ListOfTask()
	{
		
		
		_taskService.Setup(service => service.GetAll(It.IsAny<WorkItemFilter>(), It.IsAny<Paggination>()))
			.Returns(new PagedResponse<WorkItemDto>());
		
		_taskController = new WorkItemController(_taskService.Object);
	}
}