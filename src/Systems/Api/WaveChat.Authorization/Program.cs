using WaveChat.Authorization.Configuration;
using WaveChat.Services.Logger;
using WaveChat.Services.Settings;
using WaveChat.Settings;
using WaveChat.Context;
using WaveChat.Context.Setup;
using WaveChat.Api.Configuration;
using WaveChat.Authorization;

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
var identitySettings = Settings.Load<IdentitySettings>("Identity");

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger(mainSettings,logSettings);

builder.Services.AddHttpContextAccessor();

builder.Services.AddAppDbContext(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAppCors();

builder.Services.AddAppAutoMappers();
builder.Services.AddAppValidator();

builder.Services.AddAppAuth(identitySettings);

builder.Services.AddAppVersioning();
builder.Services.AddAppSwagger(mainSettings,swaggerSettings,identitySettings);

builder.Services.AddAppCors();
builder.Services.AddAppControllerAndViews();
builder.Services.AddAppHealthChecks();

//builder.Services.AddUserAccountService();
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

app.UseStaticFiles();

app.UseAppAuth();
app.UseAppCors();
app.UseAppHealthChecks();
app.UseAppSwagger();
app.UseAppControllerAndViews();

DbInitializer.Execute(app.Services);
//DbSeeder.Execute(app.Services);

app.Run();
