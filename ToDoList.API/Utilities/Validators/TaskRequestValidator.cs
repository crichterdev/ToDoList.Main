using FluentValidation;
using ToDoList.Contracts.Tasks;

namespace ToDoList.API.Utilities.Validators;

public class TaskRequestValidator : AbstractValidator<TaskRequest>
{
    public TaskRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100)
            .MinimumLength(10);
    }
}
