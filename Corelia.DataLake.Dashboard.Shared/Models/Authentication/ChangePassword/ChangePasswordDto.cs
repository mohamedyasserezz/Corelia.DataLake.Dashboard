namespace Corelia.DataLake.Dashboard.Shared.Models.Authentication.ChangePassword
{
    public record ChangePasswordDto(
        string CurrentPassword,
        string NewPassword
        );

}
