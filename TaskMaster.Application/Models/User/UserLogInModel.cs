using System.ComponentModel.DataAnnotations;

namespace TaskMaster.Application.Models.User;

public class UserLogInModel
{
	[Required(ErrorMessage = "User name is required")]
	public string? UserName { get; set; }
	[Required(ErrorMessage = "Password name is required")]
	public string? Password { get; set; }
}