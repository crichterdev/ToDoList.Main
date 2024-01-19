using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Persistance;

namespace ToDoList.Infrastructure.Persistance;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new() 
    {
        new()
        {
            Id = Guid.NewGuid(),
            FirstName = "Jorge",
            LastName = "Wilstermann",
            Email = "hola@test.com",
            Password = "1234",
        }

    };

    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _users.SingleOrDefault(u => u.Email == email);
    }
}