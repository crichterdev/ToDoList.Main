using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Application.Services.Authentication;

namespace ToDoList.Application.Interfaces;
public interface IAuthenticationService
{
    AuthenticationResult Login(string email, string password);
}
