using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Domain.Entities.Task> Tasks { get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public override int SaveChanges()
    {

        return base.SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        var seedData = LoadSeedData();
        modelBuilder.Entity<User>().HasData(seedData);
        modelBuilder.Entity<Domain.Entities.Task>()
            .ToTable(nameof(Domain.Entities.Task))
            .Property(b => b.Title)
            .IsRequired(); ;
    }

    private User LoadSeedData()
    {
        // Lee el archivo JSON y deserializa los datos
            var json = File.ReadAllText("..\\ToDoList.Infrastructure\\seedData.json");
            return JsonConvert.DeserializeObject<User>(json);
    }

}
