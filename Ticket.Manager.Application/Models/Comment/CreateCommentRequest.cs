using System.ComponentModel.DataAnnotations;

namespace Issues.Manager.Application.DTOs.Comment;

public class CreateCommentRequest
{
    [Required(ErrorMessage = "Comment can not be null"), DataType(DataType.Text)]
    public string? Content { get; set; }
}