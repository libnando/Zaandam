using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Zaandam.Api.Configuration;
using Zaandam.Domain.DTOs.Responses;
using Zaandam.Infrastructure.Contexts;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var enviroment = builder.Environment;

builder.Services.AddControllers()
     .ConfigureApiBehaviorOptions(options =>
     {
         options.InvalidModelStateResponseFactory = context =>
             new BadRequestObjectResult(new ZResponse<ErrorResponse>(new ErrorResponse("Bad Request")))
             {
                 ContentTypes =
                 {
                    Application.Json
                 }
             };
     });
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

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = Application.Json;

        var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;

        if (exception != null)        
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ZResponse<ErrorResponse>(new ErrorResponse(exception.Message))));        
    });
});


app.Run();

public partial class Program { }