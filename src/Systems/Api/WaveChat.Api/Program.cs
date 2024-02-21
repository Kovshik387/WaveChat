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

app.Run(async (context) =>
{
    var response = context.Response;
    response.ContentType = "text/html; charset=utf-8";
    await response.WriteAsync("<h1>Hello WaveChat!</h1");
});

app.Run();
