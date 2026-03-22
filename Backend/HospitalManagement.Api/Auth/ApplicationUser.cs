using Microsoft.AspNetCore.Identity;

public sealed class ApplicationUser : IdentityUser
{
    public bool EnableNotification {get; set;}

    public string Initials {get; set;} = string.Empty;

}