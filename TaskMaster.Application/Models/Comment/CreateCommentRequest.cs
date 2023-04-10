using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Application.Models.Comment;

public class CreateCommentRequest
{
	[Required(ErrorMessage = "Comment can not be null"), DataType(DataType.Text)]
	public string? Content { get; set; }
}