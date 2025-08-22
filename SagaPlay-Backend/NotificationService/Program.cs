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
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed(_ => true); // permissive for dev
    });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.MapHub<NotificationHub>("/hubs/notification");

app.Run();
