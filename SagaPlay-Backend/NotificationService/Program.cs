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

builder.Services.AddScoped<INotifyService, EmailNotifyService>();
builder.Services.AddScoped<INotifyService, PushNotifyService>();
builder.Services.AddScoped<INotificationDispatcher, NotificationDispatcher>();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
