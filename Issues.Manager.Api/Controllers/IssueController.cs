using System.Security.Claims;
using Issues.Manager.Business.Abstractions.LoggerContract;
using Issues.Manager.Business.DTOs;
using Issues.Manager.Business.Services;
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
        public ActionResult<IEnumerable<IssueDto>>  Get()
        {  _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _loggerManager.LogDebug("Getting All The Issues");
            return Ok(_issueService.GetAll(_userId));
        }

        // GET: api/Issue/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<IssueDto> Get(int id)
        {
            _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _loggerManager.LogDebug($"Trying to get issue ID:{id}");
            var issueDto = _issueService.GetById(id , _userId);
            return Ok(issueDto);
        }

        // POST: api/Issue
        [HttpPost]
        public ActionResult<IssueDto>  Post([FromBody] CreateIssueDto createdIssueDto)
        {
            _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result =  _issueService.Create(createdIssueDto, _userId);
            _loggerManager.LogDebug("Creating Issue");
            return Ok(result);
        }

        // PUT: api/Issue/5
        [HttpPut("{id}")]
        public ActionResult<IssueDto>  Put(int id, [FromBody] IssueDto issueDtoToUpdate)
        {
            _loggerManager.LogDebug($"Attempting to Update issue id: {id}");
            var result = _issueService.Update(issueDtoToUpdate);
            return Ok(result);
        }

        // DELETE: api/Issue/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _issueService.Delete(id, _userId);
            if (!result)
            {
                return NoContent();
            }
            return StatusCode(202);
        }
    }
}
