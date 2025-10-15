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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://mango-wave-09826c400.3.azurestaticapps.net", "http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
app.UseSwagger();
    app.UseSwaggerUI();


//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
