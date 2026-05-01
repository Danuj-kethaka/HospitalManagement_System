using System;
using System.ComponentModel.DataAnnotations;

public class UserDetails
{
    public string? Id { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Initials are required")]
    public string Initials { get; set; } = string.Empty;

    public string? Password { get; set; }

    public bool EnableNotification { get; set; } = true;

    public string? Role { get; set; }

}