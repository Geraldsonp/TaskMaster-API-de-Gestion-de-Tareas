using Issues.Manager.Application.DTOs.Comment;
using Issues.Manager.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Issues.Manager.Api.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Issue/{id:int}/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet]
        public IActionResult GetAll(int id)
        {
            var comments = _commentService.Get(id);
            return Ok(comments);
        }

        [HttpPost]
        public IActionResult Create(CreateCommentRequest comment, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(comment);
            }
            var commentResponse = _commentService.Create(comment, id);
            return Ok(commentResponse);
        }
    }
}
