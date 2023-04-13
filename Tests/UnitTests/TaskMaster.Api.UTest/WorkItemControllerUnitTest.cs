using AutoFixture.Xunit2;
using Bogus;
using FluentAssertions;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskMaster.Api.Contracts;
using TaskMaster.Api.Contracts.Responses;
using TaskMaster.Api.Controllers;
using TaskMaster.Application.ExtensionMethods;
using TaskMaster.Application.WorkItemFeature;
using TaskMaster.Application.WorkItemFeature.Dtos;
using TaskMaster.Domain.Entities;
using TaskMaster.Domain.Enums;
using TaskMaster.Domain.ValueObjects;

namespace TaskMaster.Api.UTests;

public class WorkItemControllerUnitTest
{
    private Mock<IWorkItemService> _taskService;
    private WorkItemController? _taskController;
    public static IEnumerable<object[]> GoodData = new[] {
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
    public static IEnumerable<object[]> QuerryParameter = new[] {
	    new object[]
	    {
		    new WorkItemQueryFilter()
		    {
			    Priority = "High",
			    TicketType = "Bug"
		    },
		    new Pagination()
		    {
			    PageNumber = 1,
			    PageSize = 10
		    },
		    10
		    ,
		    new Response<PagedResponse<WorkItemDto>>(new PagedResponse<WorkItemDto>()
		    {
			    TotalPages = 1,
			    HasPrevious = false,
			    HasNext = false,
			    TotalRecords = 10
		    })
	    }
    };


    public WorkItemControllerUnitTest()
    {
	    _taskService = new Mock<IWorkItemService>();
    }
    
    [Theory]
    [MemberData(nameof(GoodData))]
    public void CreateWorkItem_ShouldReturn_CorrectStatusCode(WorkItemCreateDto actual, WorkItemDto expected)
    {
	    _taskService.Setup(service => service.Create(It.IsAny<WorkItemCreateDto>())).Returns(expected);
		
	    _taskController = new WorkItemController(_taskService.Object);
	    
	    //Act
	    var result = _taskController.Post(actual);

	    result.Value.Should().Be(actual);
    }


	[Theory]
	[MemberData(nameof(QuerryParameter))]
	public void GetWorkItems_ShouldReturn_ListOfTask(WorkItemQueryFilter queryFilter, Pagination paging, int itemsCount, Response<PagedResponse<WorkItemDto>> expected)
	{

		var workItemFilter = queryFilter.Adapt<WorkItemFilter>();
		
		_taskService.Setup(service => service.GetAll(It.IsAny<WorkItemFilter>(), It.IsAny<Pagination>()))
			.Returns(GetWorkItems(itemsCount).AsQueryable().ToMappedPagedResponse<WorkItem, WorkItemDto>(paging));
		
		_taskController = new WorkItemController(_taskService.Object);

		var result = _taskController.Get(queryFilter, paging);
		var okObject = result.Result as OkObjectResult;
		var response = okObject.Value as Response<PagedResponse<WorkItemDto>>;
		
		response.IsSucess.Should().Be(expected.IsSucess);
		response.ErrorMessage.Should().Be(expected.ErrorMessage);
		response.Data.HasNext.Should().Be(expected.Data.HasNext);
		response.Data.HasPrevious.Should().Be(expected.Data.HasPrevious);
		response.Data.TotalPages.Should().Be(expected.Data.TotalPages);
		response.Data.TotalRecords.Should().Be(expected.Data.TotalRecords);
		response.Data.Items.Count().Should().Be(itemsCount);
	}

	private IEnumerable<WorkItem> GetWorkItems(int amount)
	{
		var faker = new Faker<WorkItem>();
		faker.RuleFor(x => x.WorkItemType, r => r.PickRandom<WorkItemType>());
		faker.RuleFor(x => x.Priority, r => r.PickRandom<Priority>());
		faker.RuleFor(x => x.Id, r => r.UniqueIndex);
		faker.RuleFor(x => x.Description, r => r.Lorem.Sentence(10));
		faker.RuleFor(x => x.Title, r => r.Name.JobType());

		return faker.Generate(amount);
	}
}