using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using RoadFlow.Common.Authentication;
using RoadFlow.Gateway.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationConfigurations();

builder.Services.AddApplicationAuthentication();
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// todo add cors and cfg for it in appSettings.json

await app.UseOcelot();

app.Run();