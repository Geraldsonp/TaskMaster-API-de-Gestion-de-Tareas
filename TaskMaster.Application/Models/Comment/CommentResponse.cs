namespace TaskMaster.Application.Models.Comment;

public class CommentResponse
{
	public int id { get; set; }
	public string? Content { get; set; }
	public DateTime PostedDate { get; set; }
}