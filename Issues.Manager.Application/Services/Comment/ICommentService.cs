using Issues.Manager.Application.DTOs.Comment;
using Issues.Manager.Domain.Entities;

namespace Issues.Manager.Application.Services;

public interface ICommentService
{
    IEnumerable<CommentResponse> Get(int issueId);

    CommentResponse Create(CreateCommentRequest commentRequest, int issueId);
}