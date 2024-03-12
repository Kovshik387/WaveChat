using WaveChat.Context;
using WaveChat.Identity.Configuration;
using WaveChat.IdentityServer;
using WaveChat.IdentityServer.Configuration;
using WaveChat.Services.Settings;
using WaveChat.Settings;

var builder = WebApplication.CreateBuilder(args);

var logSettings = Settings.Load<LogSettings>("Log");

builder.AddAppLogger(logSettings);

var services = builder.Services;

//services.AddAppCors();

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.AddAppHealthChecks();
services.RegisterAppServices();

services.AddIS4();

// Configure the HTTP request pipeline.

var app = builder.Build();

//app.UseAppCors();

app.UseAppHealthChecks();

app.UseIS4();

app.Run();
