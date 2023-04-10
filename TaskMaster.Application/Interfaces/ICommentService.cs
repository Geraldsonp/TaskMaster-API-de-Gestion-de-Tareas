using TaskMaster.Application.Models.Comment;

namespace TaskMaster.Application.Interfaces;

public interface ICommentService
{
	IEnumerable<CommentResponse> Get(int issueId);
	CommentResponse Create(CreateCommentRequest commentRequest, int issueId);
	void Delete(int issueId);
	CommentResponse Update(CreateCommentRequest comment, int issueId);
}