using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.Api.Contracts;
using TaskMaster.Api.Contracts.Responses;
using TaskMaster.Application.TaskEntity;
using TaskMaster.Application.TaskEntity.Dtos;
using TaskMaster.Domain.ValueObjects;

namespace TaskMaster.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TaskController : ControllerBase
	{
		private readonly ITaskEntityService _issueService;
		private readonly IMapper _mapper;

		public TaskController(ITaskEntityService taskService, IMapper mapper)
		{
			_issueService = taskService;
			_mapper = mapper;
		}

		// GET: api/Ticket
		[HttpGet]
		[ProducesResponseType(200)]
		public ActionResult<IEnumerable<TaskEntityDto>> Get([FromQuery] TicketFilterQuery ticketFilterQueryParameters, [FromQuery] Paggination pagging)
		{

			var ticketFilter = _mapper.Map<TaskFilter>(ticketFilterQueryParameters);

			var tickets = _issueService.GetAll(ticketFilter, pagging);


			return Ok(new Response<PagedResponse<TaskEntityDto>>(tickets));
		}

		[HttpGet("{id}", Name = "GetById")]
		[ProducesResponseType(200, Type = typeof(Response<TaskEntityDto>))]
		[ProducesResponseType(404)]
		public ActionResult<TaskEntityDto> Get(int id)
		{
			var issueDto = _issueService.GetById(id);
			return Ok(issueDto);
		}

		// POST: api/Ticket
		[ProducesResponseType(200, Type = typeof(TaskEntityDto))]
		[HttpPost]
		public ActionResult<TaskEntityDto> Post(TaskCreateDto createdRequest)
		{
			var result = _issueService.Create(createdRequest);
			return CreatedAtRoute("GetById", new { id = result.Id }, result);
		}

		// PUT: api/Ticket/5
		[HttpPut("{id}")]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public ActionResult Put(int id, [FromBody] TaskUpdateDto updateRequest)
		{
			_issueService.Update(id, updateRequest);
			return NoContent();
		}

		// DELETE: api/Ticket/5
		[HttpDelete("{id}")]
		[ProducesResponseType(200, Type = typeof(TaskEntityDto))]
		[ProducesResponseType(404)]
		public ActionResult Delete(int id)
		{
			_issueService.Delete(id);
			return Ok();
		}
	}
}