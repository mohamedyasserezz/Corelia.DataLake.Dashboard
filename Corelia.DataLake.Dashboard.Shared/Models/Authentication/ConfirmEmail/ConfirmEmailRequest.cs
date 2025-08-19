using System;

public record ConfirmEmailRequest(
    string Email,
   string Otp
);
