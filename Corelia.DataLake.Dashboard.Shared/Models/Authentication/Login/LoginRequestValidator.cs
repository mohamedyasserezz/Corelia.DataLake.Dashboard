using FluentValidation;

namespace Corelia.DataLake.Dashboard.Shared.Models.Authentication.Login
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(X => X.Email)
                .NotEmpty()
                .WithMessage("Plz Add a {PropertyName}")
                .EmailAddress()
                .WithMessage("Plz Add a Valid {PropertyName}");

            RuleFor(X => X.Password)
               .NotEmpty()
               .WithMessage("Plz Add a {PropertyName}");


        }

    }
}
