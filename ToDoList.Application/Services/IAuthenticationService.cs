using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Application.Services;
public interface IAuthenticationService
{  
    AuthenticationResult Login(string email, string password);
}
