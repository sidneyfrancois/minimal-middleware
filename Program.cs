using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MinimalMiddleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MessageOptions>(options => 
{
   options.CityName = "Albany";
});

var app = builder.Build();

app.UseMiddleware<LocationMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
