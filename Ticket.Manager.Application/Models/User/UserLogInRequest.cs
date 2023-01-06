﻿using System.ComponentModel.DataAnnotations;

namespace Issues.Manager.Application.DTOs;

public class UserLogInRequest
{
    [Required(ErrorMessage = "User name is required")]
    public string? UserName { get; set; }
    [Required(ErrorMessage = "Password name is required")]
    public string? Password { get; set; }
}