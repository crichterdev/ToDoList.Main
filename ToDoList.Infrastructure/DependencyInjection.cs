using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Infrastructure.Authentication;
using ToDoList.Infrastructure.Services;
using ToDoList.Infrastructure.Persistance;
using ToDoList.Domain.Persistance;
using Microsoft.EntityFrameworkCore;
using ToDoList.Application.Interfaces;
using ToDoList.Application.Interfaces.Common;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        return services;
    }
}
