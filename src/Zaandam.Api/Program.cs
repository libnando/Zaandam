using Zaandam.Api.Configuration;
using Zaandam.Infrastructure.Contexts;
using Zaandam.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var enviroment = builder.Environment;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistence(configuration, enviroment);
builder.Services.AddApplication();
builder.Services.AddVersioning();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SqlContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
//app.UseMiddleware<AuthMiddleware>(configuration.GetValue<string>("authKey"));

app.Run();

public partial class Program { }