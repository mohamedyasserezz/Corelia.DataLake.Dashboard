using FluentValidation;

namespace Corelia.DataLake.Dashboard.Shared.Models.Task
{
    public class TaskRequestValidator : AbstractValidator<TaskRequest>
    {
        public TaskRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id must be greater than 0");
        }
    }
}
