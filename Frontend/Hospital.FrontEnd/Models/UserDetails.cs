using System;
using System.ComponentModel.DataAnnotations;

public class UserDetails
{
    public int Id {get; set;}

    [Required(ErrorMessage = "The Initials field is required")]
    public required string Initials {get; set;}
    
    [Required(ErrorMessage = "The Email field is required")]
    public required string Email{get; set;}
    
    [Required(ErrorMessage = "The Password field is required")]
    public required string Password{get; set;}

}