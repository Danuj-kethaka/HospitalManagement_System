using System;
using System.ComponentModel.DataAnnotations;

namespace Hospital.FrontEnd.Models;

public class LoginDetails
{
    [Required(ErrorMessage = "Email is required")]
    public string Email {get; set;} = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password {get; set;} = string.Empty;

}
