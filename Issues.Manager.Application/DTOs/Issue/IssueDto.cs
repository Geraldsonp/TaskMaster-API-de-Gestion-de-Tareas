using System.ComponentModel.DataAnnotations;
using Issues.Manager.Domain.Enums;
using Swashbuckle.AspNetCore.Annotations;

namespace Issues.Manager.Application.DTOs;

[SwaggerSchema(Required = new[] { "Issue" }, Title = "Issue")]

public class IssueDto
{
    [SwaggerSchema("The Issue identifier", ReadOnly = true)]
    public int Id { get; set; }
    [SwaggerSchema("The Issue Title")]
    [Required(ErrorMessage = "Title can not be null.")]
    [MaxLength(50, ErrorMessage = "Max lenght for Title is 50 characters")]
    public string? Title { get; init; }
    [Required(ErrorMessage = "Title can not be null.")]
    [MaxLength(300, ErrorMessage = "Max lenght for description is 100 characters")]
    public string? Description { get; set; }
    [Required, Range(0,3, ErrorMessage = "Issue priority does not exist")]
    public Priority Priority { get; set; }
    [Required, Range(0,3, ErrorMessage = "Issue type does not exist")]
    public IssueType IssueType { get; set; }
    [SwaggerSchema("The Issue Creation Date", ReadOnly = true)]
    public DateTime Created { get; init; }
    [SwaggerSchema("The Issue Completion Date ", ReadOnly = true)]
    public DateTime? CompletedAt { get; set; }
    public int UserId { get; set; }
    public bool IsCompleted;
}