using FluentValidation;

namespace Corelia.DataLake.Dashboard.Shared.Models.Authentication.RefreshToken
{
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(X => X.Token)
                .NotEmpty();

            RuleFor(X => X.RefreshToken)
               .NotEmpty();
        }
    }
}
