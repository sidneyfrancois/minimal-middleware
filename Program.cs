using Microsoft.AspNetCore.Builder;
using MinimalMiddleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<QueryStringMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
