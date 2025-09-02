using Microsoft.EntityFrameworkCore;
using UserService.Database;
using UserService.Repositories;
using UserService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("UserDatabase")));

builder.Services.AddScoped<UserService.Services.IUserService, UserService.Services.UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddCors();
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseCors(x => x.AllowAnyHeader()
.AllowAnyMethod()
.WithOrigins("http://localhost:4200"));


app.MapControllers();

app.Run();
