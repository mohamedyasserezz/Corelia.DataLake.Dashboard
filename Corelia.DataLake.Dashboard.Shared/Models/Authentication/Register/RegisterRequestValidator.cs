using Corelia.DataLake.Dashboard.Shared._Common.Consts;
using FluentValidation;

namespace Corelia.DataLake.Dashboard.Shared.Models.Authentication.Register
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {


            RuleFor(X => X.FullName)
                .NotEmpty()
                .WithMessage("Plz Add a {PropertyName}")
                .Length(3, 100)
                .WithMessage("{PropertyName} length should be between 3 and 100");

            RuleFor(X => X.Email)
                .NotEmpty()
                .WithMessage(errorMessage: "Plz Add a {PropertyName}")
                .EmailAddress();


            RuleFor(X => X.Password)
                .NotEmpty()
                .WithMessage("Plz Add a {PropertyName}")
                .Matches(RegexPatterns.Password)
                .WithMessage("Password should be atleast 8 digits and contains LowerCase, UpperCase, NonAlphanumeric");
            RuleFor(X => X.UserType)
                .NotEmpty()
                .WithMessage("Plz Add a {PropertyName}")
                .Must(x => x == Users.Admin || x == Users.ProjectManager
                || x == Users.Annotator)
                .WithMessage("UserType should be either User or Admin");

        }
    }
}
