using Issues.Manager.Api.ActionFilters;
using Issues.Manager.Application.DTOs.Comment;
using Issues.Manager.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Issues.Manager.Api.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommentResponse>))]
        public IActionResult GetAll(int issueId)
        {
            var comments = _commentService.Get(issueId);
            return Ok(comments);
        }

        [HttpPost("{issueId:int}")]
        [ProducesResponseType(200, Type = typeof(CommentResponse))]
        [ProducesResponseType(422)]
        [ServiceFilter(typeof(IsModelValidFilterAttribute))]
        public IActionResult Create(CreateCommentRequest comment, [FromRoute] int issueId)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(comment);
            }
            var commentResponse = _commentService.Create(comment, issueId);
            return Ok(commentResponse);
        }

        [HttpPut("{commentId:int}")]
        [ProducesResponseType(200, Type = typeof(CommentResponse))]
        [ProducesResponseType(422)]
        [ServiceFilter(typeof(IsModelValidFilterAttribute))]
        public IActionResult Update(CreateCommentRequest comment, [FromRoute] int commentId)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(comment);
            }

            var commentResponse = _commentService.Update(comment, commentId);

            return Ok(commentResponse);
        }

        [HttpDelete("{commentId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete([FromRoute] int commentId)
        {
            _commentService.Delete(commentId);
            return NoContent();
        }
    }
}