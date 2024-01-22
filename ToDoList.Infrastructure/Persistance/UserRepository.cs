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
    private readonly ApplicationDbContext _appDbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _appDbContext = applicationDbContext;

    }

    public User? GetUserByEmail(string email)
    {
        return _appDbContext.User.SingleOrDefault(u => u.Email == email);
    }
}