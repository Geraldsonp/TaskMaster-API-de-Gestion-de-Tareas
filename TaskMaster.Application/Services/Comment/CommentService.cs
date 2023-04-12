using Mapster;
using TaskMaster.Application.Contracts;
using TaskMaster.Application.Interfaces;
using TaskMaster.Application.Models.Comment;
using TaskMaster.Domain.Exceptions;

namespace TaskMaster.Application.Services.Comment;

public class CommentService : ICommentService
{
	private readonly IUnitOfWork _repositoryManager;


	public CommentService(IUnitOfWork repositoryManager)
	{
		_repositoryManager = repositoryManager;
	}
	public CommentResponse Create(CreateCommentRequest commentRequest, int taskId)
	{
		var issue = _repositoryManager.TaskRepository.FindByCondition(i => i.Id == taskId, true);

		if (issue is null)
		{
			throw new NotFoundException(nameof(Domain.Entities.TaskEntity), taskId);
		}

		var comment = commentRequest.Adapt<Domain.Entities.Comment>();

		issue.Comments.Add(comment);

		_repositoryManager.SaveChanges();

		var commentReponse = comment.Adapt<CommentResponse>();

		return commentReponse;
	}

	//Todo: Implement Returning this for all the services
	public void Delete(int commentId)
	{
		var comment = _repositoryManager.CommentsRepository.FindByCondition(c => c.Id == commentId);

		if (comment is null)
		{
			throw new NotFoundException(nameof(Domain.Entities.Comment), commentId);
		}

		_repositoryManager.CommentsRepository.Delete(comment);
	}

	public CommentResponse Update(CreateCommentRequest updatedComment, int commentId)
	{
		var comment = _repositoryManager.CommentsRepository.FindByCondition(comment => comment.Id == commentId, true);

		if (comment is null)
		{
			throw new NotFoundException(nameof(Domain.Entities.Comment), commentId);
		}

		comment.Content = updatedComment.Content;

		_repositoryManager.SaveChanges();

		var commentReponse = comment.Adapt<CommentResponse>();

		return commentReponse;
	}

	public IEnumerable<CommentResponse> Get(int issueId)
	{
		var issue = _repositoryManager.TaskRepository.FindByCondition(i => i.Id == issueId);

		if (issue is null)
		{
			throw new NotFoundException(nameof(Domain.Entities.TaskEntity), issueId);
		}

		var comments = _repositoryManager.CommentsRepository
			.FindRangeByCondition(c => c.Ticket.Id == issueId).ToList();

		return comments.Adapt<IEnumerable<CommentResponse>>();
	}
}