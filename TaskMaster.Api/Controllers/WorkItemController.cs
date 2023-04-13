using System.Net;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.Api.Contracts;
using TaskMaster.Api.Contracts.Responses;
using TaskMaster.Application.WorkItemFeature;
using TaskMaster.Application.WorkItemFeature.Dtos;
using TaskMaster.Domain.ValueObjects;

namespace TaskMaster.Api.Controllers
{
	[Route("api/work-items")]
	[ApiController]
	public class WorkItemController : ControllerBase
	{
		private readonly IWorkItemService _issueService;

		public WorkItemController(IWorkItemService taskService)
		{
			_issueService = taskService;
		}

		// GET: api/Ticket
		[HttpGet]
		[ProducesResponseType(200)]
		public ActionResult<IEnumerable<WorkItemDto>> Get([FromQuery] WorkItemQueryFilter taskFilter, [FromQuery] Paggination pagging)
		{

			var ticketFilter = taskFilter.Adapt<WorkItemFilter>();

			var tickets = _issueService.GetAll(ticketFilter, pagging);
			
			return Ok(new Response<PagedResponse<WorkItemDto>>(tickets));
		}

		[HttpGet("{id}", Name = "GetById")]
		[ProducesResponseType(200, Type = typeof(Response<WorkItemDto>))]
		[ProducesResponseType(404)]
		public ActionResult<WorkItemDto> Get(int id)
		{
			var issueDto = _issueService.GetById(id);
			return Ok(issueDto);
		}

		// POST: api/Task
		[ProducesResponseType(200, Type = typeof(WorkItemDto))]
		[HttpPost]
		public ActionResult<WorkItemDto> Post(WorkItemCreateDto createdRequest)
		{
			var result = _issueService.Create(createdRequest);
			return CreatedAtRoute("GetById", new { id = result.Id }, result);
		}

		// PUT: api/Ticket/5
		[HttpPut("{id}")]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public ActionResult Put(int id, [FromBody] WorkItemUpdateDto updateRequest)
		{
			_issueService.Update(id, updateRequest);
			return NoContent();
		}

		// DELETE: api/Ticket/5
		[HttpDelete("{id}")]
		[ProducesResponseType(200, Type = typeof(WorkItemDto))]
		[ProducesResponseType(404)]
		public ActionResult Delete(int id)
		{
			_issueService.Delete(id);
			return Ok();
		}
	}
}