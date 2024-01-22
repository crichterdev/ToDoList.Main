using Microsoft.EntityFrameworkCore;
using ToDoList.Application;
using ToDoList.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply Migrations
ApplyMigrations(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


// Helper method to apply migrations
static void ApplyMigrations(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var dbContext = services.GetRequiredService<ApplicationDbContext>();
            // Aplica migraciones
            
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
            Console.WriteLine("Migraciones aplicadas correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al aplicar migraciones: {ex.Message}");
        }
    }
}