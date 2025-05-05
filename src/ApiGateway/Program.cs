using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "gatewayAllowOriginsCors", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader(); ;
    });
});

builder.Configuration
      .SetBasePath(builder.Environment.ContentRootPath)
      .AddOcelot();
builder.Services
    .AddOcelot(builder.Configuration);

var app = builder.Build();
app.UseCors("gatewayAllowOriginsCors");
await app.UseOcelot();
await app.RunAsync();