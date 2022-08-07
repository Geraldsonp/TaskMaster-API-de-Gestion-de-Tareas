using AutoMapper;
using Issues.Manager.Application.Abstractions.RepositoryContracts;
using Issues.Manager.Application.DTOs.Comment;
using Issues.Manager.Domain.Entities;
using Issues.Manager.Domain.Exceptions;
using Issues.Manager.Domain.ValueObjects;

namespace Issues.Manager.Application.Services;

public class CommentService : ICommentService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;


    public CommentService(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }
    public CommentResponse Create(CreateCommentRequest commentRequest, int issueId)
    {
        var issue = _repositoryManager.Issue.FindByCondition(i => i.Id == issueId, true);
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
    public void Delete(int issueId, int commentId)
    {
        var comment = _repositoryManager.Comment.FindByCondition(c => c.Id == commentId && c.Issue.Id == issueId);
        if (comment is null)
        {
            throw new IssueNotFoundException(commentId);
        }
        _repositoryManager.Comment.Delete(comment);
    }

    public IEnumerable<CommentResponse> Get(int issueId)
    {
        var issue = _repositoryManager.Issue.FindByCondition(i => i.Id == issueId);

        if (issue is null)
        {
            throw new IssueNotFoundException(issueId);
        }
        var comments = _repositoryManager.Comment
            .FindRangeByCondition(c => c.Issue.Id == issueId).ToList();
        return _mapper.Map<IEnumerable<CommentResponse>>(comments); 
    }
}