using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoList.API.Utilities.Validators;
using ToDoList.Application;
using ToDoList.Infrastructure;
using ToDoList.Infrastructure.Authentication;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers(options =>
{
    //options.Filters.Add(typeof(ValidateTaskRequestFilter));
    //options.Filters.Add(typeof(ValidateLoginRequestFilter));
});
builder.Services.AddTransient<ValidateTaskRequestFilter>();
builder.Services.AddTransient<ValidateLoginRequestFilter>();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.MapInboundClaims = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "CRichter",
        ValidAudience = "ToDoListUsers",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tech-asessment-carlos-richter-rodriguez")),
};
 
});

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

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

app.UseAuthentication();
app.UseAuthorization();

// Enable CORS
app.UseCors();

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
            // Aply migraciones
            
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
           
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An Error ocurred while apliyng migration: {ex.Message}");
        }
    }
}