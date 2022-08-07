using Issues.Manager.Application.DTOs.Comment;
using Issues.Manager.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Issues.Manager.Api.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Issue/{issueId:int}/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet]
        public IActionResult GetAll(int issueId)
        {
            var comments = _commentService.Get(issueId);
            return Ok(comments);
        }

        [HttpPost]
        public IActionResult Create(CreateCommentRequest comment, [FromRoute] int issueId)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(comment);
            }
            var commentResponse = _commentService.Create(comment, issueId);
            return Ok(commentResponse);
        }

        [HttpDelete("{commentId}")]
        public IActionResult Delete([FromRoute] int commentId, int issueId)
        {
            _commentService.Delete(issueId, commentId);
            return Ok();
        }
    }
}
