using System.ComponentModel.DataAnnotations;

namespace Issues.Manager.Application.DTOs;

public class UserRegistrationDto
{
        [Required(ErrorMessage = "First name is Required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "First name is Required")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        [Required, DataType(DataType.EmailAddress, ErrorMessage = "This is not a valid Email Address")]
        public string? Email { get; set; }

}