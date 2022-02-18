using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RoadFlow.Common.Auth;
using RoadFlow.Common.Configurations;
using RoadFlow.Common.Errors;
using RoadFlow.Common.Mediator;
using RoadFlow.Data;
using RoadFlow.Identity.API.IoC;
using RoadFlow.Identity.Core;

var builder = WebApplication.CreateBuilder(args);

builder.SetupApplicationConfigurations();
var (jwtConfiguration, googleAuthConfiguration, mongoDbConfiguration) = SharedConfigurationBuilder.BindAndValidate(builder.Configuration);

builder.Services.AddSingleton(jwtConfiguration);
builder.Services.AddSingleton(googleAuthConfiguration);
builder.Services.AddSingleton(mongoDbConfiguration);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddAuthenticationByJwtBearer(jwtConfiguration)
    .AddAuthenticationByGoogleOAuth(googleAuthConfiguration);

builder.Services.AddSingleton<MongoContext>();

builder.Services.RegisterAppRepositories();
builder.Services.RegisterAppServices();

builder.Services.AddValidatorsFromAssembly(IndentityCoreProjectAssembly.Assembly);

builder.Services.AddMediatR(IndentityCoreProjectAssembly.Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PipelineBehavior<,>));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();