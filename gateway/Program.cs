using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowFrontend",
//         policy => policy
//             .WithOrigins("http://localhost:4200", "https://qa-ui.lmcalc.com", "https://ui.lmcalc.com")
//             .AllowAnyHeader()
//             .AllowAnyMethod());
// });

// Load Ocelot configuration
builder.Configuration
.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

//Read origins from config
var allowedOrigins = builder.Configuration
    .GetSection("AllowedOrigins")
    .Get<string[]>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins(allowedOrigins ?? Array.Empty<string>())
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddOcelot();

var app = builder.Build();

app.UseCors("AllowFrontend");

// Enable Ocelot middleware
await app.UseOcelot();

app.Run();
