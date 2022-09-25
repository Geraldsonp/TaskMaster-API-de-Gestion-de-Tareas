using System.Security.Claims;
using Issues.Manager.Api.ActionFilters;
using Issues.Manager.Application.DTOs;
using Issues.Manager.Application.Services;
using Issues.Manager.Application.Services.Logger;
using Microsoft.AspNetCore.Mvc;

namespace Issues.Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;
        private readonly ILoggerManager _loggerManager;
        private string _userId;

        public IssueController(IIssueService issueService, ILoggerManager loggerManager)
        {
            _issueService = issueService;
            _loggerManager = loggerManager;
        }

        // GET: api/Issue
        [HttpGet]
        [ProducesResponseType(200)]
        public ActionResult<IEnumerable<IssueReponse>> Get()
        {
            _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(_issueService.GetAll());
        }

        // GET: api/Issue/5
        [HttpGet("{id}", Name = "GetById")]
        [ProducesResponseType(200, Type = typeof(IssueReponse))]
        [ProducesResponseType(404)]
        public ActionResult<IssueReponse> Get(int id)
        {
            _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var issueDto = _issueService.GetById(id);
            return Ok(issueDto);
        }

        // POST: api/Issue
        [HttpPost]
        [ServiceFilter(typeof(IsModelValidFilterAttribute))]
        public ActionResult<IssueReponse> Post([FromBody] CreateIssueRequest createdIssueRequest)
        {
            _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _issueService.Create(createdIssueRequest, _userId);
            _loggerManager.LogInfo("Creating Issue");
            return CreatedAtRoute("GetById", new { id = result.Id }, result);
        }

        // PUT: api/Issue/5
        [HttpPut("{id}")]
        [ServiceFilter(typeof(IsModelValidFilterAttribute))]
        public ActionResult<IssueReponse> Put(int id, [FromBody] IssueReponse issueReponseToUpdate)
        {
            _loggerManager.LogInfo($"Attempting to Update issue id: {id}");
            var result = _issueService.Update(issueReponseToUpdate);
            return Ok(result);
        }

        // DELETE: api/Issue/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _issueService.Delete(id);
            return Ok();
        }
    }
}