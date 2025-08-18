using FluentValidation;

namespace Corelia.DataLake.Dashboard.Shared.Models.Authentication.ChangePassword
{
    public class ChangPasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangPasswordValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required Please enter {PropertyName}.");


            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.");

            RuleFor(x => x.NewPassword)
                .NotEqual(x => x.CurrentPassword)
                .WithMessage("New password must be different from the current password.");




        }
    }
}
