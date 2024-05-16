
using WaveChat.Context;
using WaveChat.Common.Settings;
using WaveChat.Services.Settings;
using WaveChat.Storage;
using WaveChat.Storage.Configuration;
using WaveChat.Services.Settings.Settings;

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
var identitySettings = Settings.Load<AuthSettings>("Identity");
var minioSettings = Settings.Load<MinioSettings>("Minio");

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger(mainSettings, logSettings);

builder.Services.AddHttpContextAccessor();

builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.RegisterServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAppAutoMappers();
builder.Services.AddAppValidator();

builder.Services.AddAppAuth(identitySettings);

builder.Services.AddAppVersioning();
builder.Services.AddAppSwagger(mainSettings, swaggerSettings, identitySettings);
builder.Services.AddAppMinio(minioSettings);

builder.Services.AddAppCors();
builder.Services.AddAppControllerAndViews();
builder.Services.AddAppHealthChecks();

var app = builder.Build();

app.UseStaticFiles();

app.UseAppAuth();
app.UseAppCors();
app.UseAppHealthChecks();
app.UseAppSwagger();
app.UseAppControllerAndViews();

app.Run();