using WaveChat.Api.Configuration;
using WaveChat.Authorization;
using WaveChat.Authorization.Configuration;
using WaveChat.Services.Logger;
using WaveChat.Services.Settings;
using WaveChat.Settings;
using WaveChat.Context;
using WaveChat.Context.Setup;
using WaveChat.Context.Seeder.Seeds;

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
var identitySettings = Settings.Load<IdentitySettings>("Identity");

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger(mainSettings,logSettings);

builder.Services.AddAppDbContext(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAppAutoMappers();
builder.Services.AddAppValidator();

builder.Services.AddAppVersioning();
builder.Services.AddAppSwagger(mainSettings,swaggerSettings,identitySettings);

builder.Services.AddAppCors();
builder.Services.AddAppControllerAndViews();

builder.Services.AddAppHealthChecks();
builder.Services.RegisterService();

var app = builder.Build();

app.UseStaticFiles();

app.UseAppCors();
app.UseAppHealthChecks();
app.UseAppSwagger();
app.UseAppControllerAndViews();

//DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services);

app.Run();
