namespace Pollifyr.App.Models.Forms.Auth;

public class ChangePasswordForm
{
    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }

    public string RepeatNewPassword { get; set; }
}