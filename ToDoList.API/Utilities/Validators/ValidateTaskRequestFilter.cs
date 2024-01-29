using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Contracts.Tasks;

namespace ToDoList.API.Utilities.Validators;

public class ValidateTaskRequestFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.TryGetValue("taskRequest", out var taskRequest))
        {
            var validator = new TaskRequestValidator();
            if (taskRequest != null)
            {
                
                var validationResult = await validator.ValidateAsync((TaskRequest)taskRequest);
                if (!validationResult.IsValid)
                {
                    context.Result = new BadRequestObjectResult(validationResult.Errors);
                    return;
                }
            }
        }

        await next();
    }
}