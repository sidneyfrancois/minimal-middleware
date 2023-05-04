using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace MinimalMiddleware;

public class LocationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly MessageOptions _options;

    public LocationMiddleware(RequestDelegate nextDelegate, IOptions<MessageOptions> opts)
    {
        _next = nextDelegate;
        _options = opts.Value;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path == "/location")
        {
            await context.Response.WriteAsync($"{_options.CityName}, {_options.CountryName}");
        }
        else
        {
            await _next(context);
        }
    }
}