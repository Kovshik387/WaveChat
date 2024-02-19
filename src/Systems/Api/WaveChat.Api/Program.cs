using WaveChat.Api.Configuration;
using WaveChat.Authorization;
using WaveChat.Authorization.Configuration;
using WaveChat.Services.Logger;
using WaveChat.Services.Settings;
using WaveChat.Settings;

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
var identitySettings = Settings.Load<IdentitySettings>("Identity");

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger(mainSettings,logSettings);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAppAutoMappers();
builder.Services.AddAppValidator();


builder.Services.AddAppVersioning();
builder.Services.AddAppSwagger(mainSettings,swaggerSettings);

builder.Services.AddAppCors();
builder.Services.AddAppControllerAndViews();

builder.Services.AddAppHealthChecks();
builder.Services.RegisterService();

var app = builder.Build();

app.UseStaticFiles().UseDefaultFiles();
app.UseAppCors();
app.UseAppHealthChecks();
app.UseAppControllerAndViews();
app.UseRouting();
app.UseAppSwagger();

app.Run();
