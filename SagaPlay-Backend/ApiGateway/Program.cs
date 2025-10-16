using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Load environment-specific Ocelot config
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var config = new ConfigurationBuilder()
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"ocelot.{environment}.json", optional: true, reloadOnChange: true)
    .Build();

builder.Configuration.AddConfiguration(config);

builder.Services.AddOcelot(builder.Configuration);

//Add Okta authentication to handle JWTs
builder.Services
    .AddAuthentication("Okta")
    .AddJwtBearer("Okta", options =>
    {
        // Your Okta domain (the one ending with auth0.com or okta.com)
        options.Authority = "https://dev-sagaplay.eu.auth0.com/";

        // The "Audience" is the API Identifier you set in Okta
        // (when you configure your API in the Okta dashboard)
        options.Audience = "https://sagaplay/api"; // replace with yours
    });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

app.Run();

public partial class Program { }
