namespace Corelia.DataLake.Dashboard.Shared.Models.Authentication.RefreshToken
{
    public record RefreshTokenRequest(
        string Token,
        string RefreshToken
        );
}
