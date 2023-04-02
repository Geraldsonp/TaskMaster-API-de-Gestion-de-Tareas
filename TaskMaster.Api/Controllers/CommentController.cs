using Issues.Manager.Application.DTOs.Comment;
using Issues.Manager.Application.Services;
using Microsoft.AspNetCore.Mvc;
using TaskMaster.Api.Contracts.Responses;

namespace Issues.Manager.Api.Controllers
{
	[Route("api/ticket/{ticketId:int}/[controller]")]
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
		public IActionResult GetAll(int ticketId)
		{
			var comments = _commentService.Get(ticketId);
			return Ok(comments);
		}

		[HttpPost]
		[ProducesResponseType(200, Type = typeof(Response<CommentResponse>))]
		[ProducesResponseType(422)]
		public IActionResult Create(CreateCommentRequest comment, [FromRoute] int ticketId)
		{
			if (!ModelState.IsValid)
			{
				return UnprocessableEntity(comment);
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
				return UnprocessableEntity(comment);
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