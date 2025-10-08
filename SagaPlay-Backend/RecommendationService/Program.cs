using Microsoft.Extensions.Options;
using RecommendationService.Services;
using RecommendationService.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<CatalogServiceOptions>(
    builder.Configuration.GetSection("CatalogService"));

builder.Services.AddHttpClient<IRecommendationService, RecommendationService.Services.RecommendationService>((sp, client) =>
{
    var options = sp.GetRequiredService<IOptions<CatalogServiceOptions>>().Value;
    client.BaseAddress = new Uri(options.BaseUrl);    
});

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader()
.AllowAnyMethod()
.WithOrigins("http://localhost:4200"));

// Configure the HTTP request pipeline.
app.UseSwagger();
    app.UseSwaggerUI();


//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
