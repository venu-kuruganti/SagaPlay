using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

//builder.Services
//    .AddAuthentication("Okta")
//    .AddJwtBearer("Okta", options =>
//    {
//        // Your Okta domain (the one ending with auth0.com or okta.com)
//        options.Authority = "https://dev-sagaplay.eu.auth0.com/";

//        // The "Audience" is the API Identifier you set in Okta
//        // (when you configure your API in the Okta dashboard)
//        options.Audience = "https://sagaplay/api"; // replace with yours
//    });

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

//app.UseAuthentication();
//app.UseAuthorization();

await app.UseOcelot();


app.Run();

public partial class Program { }
