using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Utilities.Validators;
using ToDoList.Application.Interfaces;
using ToDoList.Contracts.Authentication;

namespace ToDoList.API.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {

        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    [ServiceFilter(typeof(ValidateLoginRequestFilter))]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(
            request.Email,
            request.Password);

        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
            );

        return Ok(response);
    }
}
