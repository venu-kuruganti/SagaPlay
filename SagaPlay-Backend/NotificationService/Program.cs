using NotificationService.Services;
using NotificationService.Utilities;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMailKit(config =>
{
    config.UseMailKit(builder.Configuration.GetSection("EmailSettings").Get<MailKitOptions>());
});

builder.Services.AddSignalR();

builder.Services.AddScoped<INotifyService, EmailNotifyService>();
builder.Services.AddScoped<INotifyService, PushNotifyService>();
builder.Services.AddScoped<INotificationDispatcher, NotificationDispatcher>();


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
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}



app.MapControllers();


app.MapHub<NotificationHub>("/hubs/notification");

app.Run();
