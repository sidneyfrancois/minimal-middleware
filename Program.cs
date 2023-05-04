
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MinimalMiddleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) => {
   await next();
   await context.Response
      .WriteAsync($"\nStatus Code: { context.Response.StatusCode}");
});

app.Use(async (context, next) => {
   if (context.Request.Path == "/short")
   {
      await context.Response
         .WriteAsync($"Request short circuit middleware");
   } 
   else 
   {
      await next();
   }
});

app.Use(async (context, next) => {
   if (context.Request.Method == HttpMethods.Get
   && context.Request.Query["custom"] == "true") 
   {
      context.Response.ContentType = "text/plain";
      await context.Response.WriteAsync("Custom Middleware \n");
   }

   await next();
});


((IApplicationBuilder) app).Map("/branch", branch => 
{
   branch.UseMiddleware<QueryStringMiddleware>();

   branch.Run(async (context) =>
   {
      await context.Response.WriteAsync($"Branch middleware");
   }); 
});

((IApplicationBuilder)app).Map("/branch-terminal", branch =>
{
   branch.Run(new QueryStringMiddleware().Invoke);
});

app.UseMiddleware<QueryStringMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
