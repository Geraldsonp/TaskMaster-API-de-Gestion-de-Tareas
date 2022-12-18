namespace Issues.Manager.Application.DTOs.Comment;

public class CommentResponse
{
    public int id { get; set; }
    public string? Content { get; set; }
    public DateTime PostedDate { get; set; }
}