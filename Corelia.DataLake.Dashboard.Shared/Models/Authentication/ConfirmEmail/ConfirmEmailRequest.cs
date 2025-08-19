using System;

namespace Corelia.DataLake.Dashboard.Shared.Models.Authentication.ConfirmEmail
{ 
public record ConfirmEmailRequest(
    string Email,
   string Otp
);
}