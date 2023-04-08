using AutoMapper;
using Issues.Manager.Application.Contracts;
using Issues.Manager.Application.DTOs.Comment;
using Issues.Manager.Domain.Entities;
using Issues.Manager.Domain.Exceptions;

namespace Issues.Manager.Application.Services;

public class CommentService : ICommentService
{
	private readonly IUnitOfWork _repositoryManager;
	private readonly IMapper _mapper;


	public CommentService(IUnitOfWork repositoryManager, IMapper mapper)
	{
		_repositoryManager = repositoryManager;
		_mapper = mapper;
	}
	public CommentResponse Create(CreateCommentRequest commentRequest, int issueId)
	{
		var issue = _repositoryManager.TaskRepository.FindByCondition(i => i.Id == issueId, true);

		if (issue is null)
		{
			throw new IssueNotFoundException(issueId);
		}

		var comment = _mapper.Map<Comment>(commentRequest);

		issue.Comments.Add(comment);

		_repositoryManager.SaveChanges();

		var commentReponse = _mapper.Map<CommentResponse>(comment);

		return commentReponse;
	}

	//Todo: Implement Returning this for all the services
	public void Delete(int commentId)
	{
		var comment = _repositoryManager.CommentsRepository.FindByCondition(c => c.Id == commentId);

		if (comment is null)
		{
			throw new IssueNotFoundException(commentId);
		}

		_repositoryManager.CommentsRepository.Delete(comment);
	}

	public CommentResponse Update(CreateCommentRequest updatedComment, int commentId)
	{
		var comment = _repositoryManager.CommentsRepository.FindByCondition(comment => comment.Id == commentId, true);

		if (comment is null)
		{
			throw new IssueNotFoundException(commentId);
		}

		comment.Content = updatedComment.Content;

		_repositoryManager.SaveChanges();

		var commentReponse = _mapper.Map<CommentResponse>(comment);

		return commentReponse;
	}

	public IEnumerable<CommentResponse> Get(int issueId)
	{
		var issue = _repositoryManager.TaskRepository.FindByCondition(i => i.Id == issueId);

		if (issue is null)
		{
			throw new IssueNotFoundException(issueId);
		}

		var comments = _repositoryManager.CommentsRepository
			.FindRangeByCondition(c => c.Ticket.Id == issueId).ToList();

		return _mapper.Map<IEnumerable<CommentResponse>>(comments);
	}
}