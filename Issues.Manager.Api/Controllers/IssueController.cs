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
            return _issueService.GetAll();
        }

        // GET: api/Issue/5
        [HttpGet("{id}", Name = "Get")]
        public IssueDto Get(int id)
        {
            var issueDto = _issueService.GetById(id);
            return issueDto;
        }

        // POST: api/Issue
        [HttpPost]
        public IssueDto Post([FromBody] CreateIssueDto createdIssueDto)
        {
            return _issueService.Create(createdIssueDto);
        }

        // PUT: api/Issue/5
        [HttpPut("{id}")]
        public IssueDto Put(int id, [FromBody] IssueDto issueDtoToUpdate)
        {
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
