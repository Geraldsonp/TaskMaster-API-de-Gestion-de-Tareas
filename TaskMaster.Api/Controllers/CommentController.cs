using Issues.Manager.Application.DTOs.Comment;
using Issues.Manager.Application.Services;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.Api.Contracts.Responses;

namespace Issues.Manager.Api.Controllers
{
	[Route("api/comments")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ICommentService _commentService;

		public CommentController(ICommentService commentService)
		{
			_commentService = commentService;
		}

		[HttpGet(template: "{taskId:int}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<CommentResponse>))]
		public IActionResult GetAll(int taskId)
		{
			var comments = _commentService.Get(taskId);
			return Ok(comments);
		}

		[HttpPost("{taskId:int}")]
		[ProducesResponseType(200, Type = typeof(Response<CommentResponse>))]
		[ProducesResponseType(422)]
		public IActionResult Create(CreateCommentRequest comment, [FromRoute] int ticketId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState.ValidationState);
			}
			var commentResponse = _commentService.Create(comment, ticketId);
			return Ok(new Response<CommentResponse>(commentResponse));
		}

		[HttpPut("{commentId:int}")]
		[ProducesResponseType(200, Type = typeof(CommentResponse))]
		[ProducesResponseType(422)]
		public IActionResult Update(CreateCommentRequest comment, [FromRoute] int commentId)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState.ValidationState);
			}

			var commentResponse = _commentService.Update(comment, commentId);

			return Ok(new Response<CommentResponse>(commentResponse));
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