using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Persistance;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}
