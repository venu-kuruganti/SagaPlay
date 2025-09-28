using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WatchlistService.Database;
using WatchlistService.Models;
using WatchlistService.Repositories;
using WatchlistService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().
    AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter<StatusEnum>());
    });

builder.Services.AddDbContext<WatchListContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("WatchListDatabase"));
});

builder.Services.AddScoped<IWLService, WLService>();
builder.Services.AddScoped<IWatchListRepository, WatchListRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();


var app = builder.Build();

app.UseCors(x =>
{
    x.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200");
});

// Configure the HTTP request pipeline.
app.UseSwagger();
    app.UseSwaggerUI();


app.MapControllers();

app.Run();
