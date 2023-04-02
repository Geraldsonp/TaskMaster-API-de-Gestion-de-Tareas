using System.Net;
using AutoMapper;
using Issues.Manager.Api.Contracts;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.Interfaces;
using Issues.Manager.Application.Models.Issue;
using Microsoft.AspNetCore.Mvc;

namespace Issues.Manager.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TicketController : ControllerBase
	{
		private readonly IIssueService _issueService;
		private readonly IMapper _mapper;
		private string _userId;

		public TicketController(IIssueService issueService, IMapper mapper)
		{
			_issueService = issueService;
			_mapper = mapper;
		}

		// GET: api/Ticket
		[HttpGet]
		[ProducesResponseType(200)]
		public ActionResult<IEnumerable<TicketDetailsModel>> Get([FromQuery] TicketFilterQuery ticketFilterQueryParameters, [FromQuery] PagingQueryParameters pagging)
		{

			var ticketFilter = _mapper.Map<TicketFilters>(ticketFilterQueryParameters);
			return Ok(_issueService.GetAll(ticketFilter, pagging));
		}

		[HttpGet("{id}", Name = "GetById")]
		[ProducesResponseType(200, Type = typeof(TicketDetailsModel))]
		[ProducesResponseType(404)]
		public ActionResult<TicketDetailsModel> Get(int id)
		{
			var issueDto = _issueService.GetById(id);
			return Ok(issueDto);
		}

		// POST: api/Ticket
		[ProducesResponseType(200, Type = typeof(TicketDetailsModel))]
		[HttpPost]
		public ActionResult<TicketDetailsModel> Post(TicketCreateRequest createdRequest)
		{
			var result = _issueService.Create(createdRequest);
			return CreatedAtRoute("GetById", new { id = result.Id }, result);
		}

		// PUT: api/Ticket/5
		[HttpPut("{id}")]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.NotFound)]
		public ActionResult Put(int id, [FromBody] TicketUpdateRequest updateRequest)
		{
			_issueService.Update(id, updateRequest);
			return NoContent();
		}

		// DELETE: api/Ticket/5
		[HttpDelete("{id}")]
		[ProducesResponseType(200, Type = typeof(TicketDetailsModel))]
		[ProducesResponseType(404)]
		public ActionResult Delete(int id)
		{
			_issueService.Delete(id);
			return Ok();
		}
	}
}