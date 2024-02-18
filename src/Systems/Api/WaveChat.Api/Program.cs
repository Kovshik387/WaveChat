using WaveChat.Api;
using WaveChat.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAppAutoMappers();
builder.Services.AddAppValidator();

builder.Services.RegisterService();

builder.Services.AddAppCors();
builder.Services.AddAppControllerAndViews();

var app = builder.Build();

app.UseAppCors();
app.UseAppControllerAndViews();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
