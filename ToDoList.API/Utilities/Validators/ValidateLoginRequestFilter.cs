using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Contracts.Authentication;

namespace ToDoList.API.Utilities.Validators;

public class ValidateLoginRequestFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.TryGetValue("request", out var loginRequest))
        {
            var validator = new LoginRequestValidator();

            if (loginRequest != null)
            {
                var validationResult = await validator.ValidateAsync((LoginRequest)loginRequest);

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
