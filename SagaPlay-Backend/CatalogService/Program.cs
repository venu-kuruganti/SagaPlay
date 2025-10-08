using CatalogService.Database;
using CatalogService.Repository;
using CatalogService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CatalogContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("CatalogDatabase")));

builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();

builder.Services.AddScoped<IInternalCatalogService, InternalCatalogService>();

builder.Services.AddCors();

var app = builder.Build();

//app.UseCors(x =>
//{
//    x.AllowAnyHeader()
//    .AllowAnyMethod()
//    .WithOrigins("http://localhost:4200");
//});

//app.UseCors(x =>
//{
//    x.AllowAnyHeader()
//    .AllowAnyMethod()
//    .WithOrigins("https://localhost:32769/", "http://localhost:32768/");
//});

//

// Configure the HTTP request pipeline.
app.UseSwagger();
    app.UseSwaggerUI();


//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
