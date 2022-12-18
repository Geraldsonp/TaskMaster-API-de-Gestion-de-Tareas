using Issues.Manager.Application.DTOs.Comment;
using Issues.Manager.Domain.Entities;
using Issues.Manager.Domain.ValueObjects;

namespace Issues.Manager.Application.Services;

public interface ICommentService
{
    IEnumerable<CommentResponse> Get(int issueId);
    CommentResponse Create(CreateCommentRequest commentRequest, int issueId);
    void Delete(int commentId, int issueId);
    CommentResponse Update(CreateCommentRequest comment, int issueId);
}