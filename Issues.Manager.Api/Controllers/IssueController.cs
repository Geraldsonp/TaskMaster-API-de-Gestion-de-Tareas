using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Issues.Manager.Business.Abstractions.LoggerContract;
using Issues.Manager.Business.DTOs;
using Issues.Manager.Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Issues.Manager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;
        private readonly ILoggerManager _loggerManager;

        public IssueController(IIssueService issueService, ILoggerManager loggerManager)
        {
            _issueService = issueService;
            _loggerManager = loggerManager;
        }
        // GET: api/Issue
        [HttpGet]
        public IEnumerable<IssueDto> Get()
        {
            _loggerManager.LogDebug("Getting All The Issues");
            return _issueService.GetAll();
        }

        // GET: api/Issue/5
        [HttpGet("{id}", Name = "Get")]
        public IssueDto Get(int id)
        {
            _loggerManager.LogDebug($"Trying to get issue ID:{id}");
            var issueDto = _issueService.GetById(id);
            return issueDto;
        }

        // POST: api/Issue
        [HttpPost]
        public IssueDto Post([FromBody] CreateIssueDto createdIssueDto)
        {
            var result =  _issueService.Create(createdIssueDto);
            _loggerManager.LogDebug("Creating Issue");
            return result;
        }

        // PUT: api/Issue/5
        [HttpPut("{id}")]
        public IssueDto Put(int id, [FromBody] IssueDto issueDtoToUpdate)
        {
            _loggerManager.LogDebug($"Attempting to Update issue id: {id}");
            var result = _issueService.Update(issueDtoToUpdate);
            return result;
        }

        // DELETE: api/Issue/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var result = _issueService.Delete(id);
            return result;
        }
    }
}
