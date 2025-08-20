namespace Corelia.DataLake.Dashboard.Shared.Models.Authentication.ChangePassword
{
    public record ChangePasswordRequest(
        string CurrentPassword,
        string NewPassword
        );

}
