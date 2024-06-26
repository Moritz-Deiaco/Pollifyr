using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Pollifyr.App.Models.Forms.Users;

public class CreateUserForm
{
    [Required(ErrorMessage = "You need to provide an email address")]
    [EmailAddress(ErrorMessage = "You need to enter a valid email address")]
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Please provide a valid email adress")]
    [Description("The email for the user")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "You need to provide an username")]
    [MinLength(6, ErrorMessage = "The username is too short")]
    [MaxLength(20, ErrorMessage = "The username cannot be longer than 20 characters")]
    [RegularExpression("^[a-z][a-z0-9]*$", ErrorMessage = "Usernames can only contain lowercase characters and numbers and should not start with a number")]
    [Description("A username for your account")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "You need to provide a password")]
    [MinLength(8, ErrorMessage = "The password must be at least 8 characters long")]
    [MaxLength(256, ErrorMessage = "The password must not be longer than 256 characters")]
    [Description("The password for the user")]
    public string Password { get; set; }
    
}