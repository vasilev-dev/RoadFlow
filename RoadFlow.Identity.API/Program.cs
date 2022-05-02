using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using RoadFlow.Common.Auth;
using RoadFlow.Common.Configurations;
using RoadFlow.Common.Errors;
using RoadFlow.Common.Mediator;
using RoadFlow.Data;
using RoadFlow.Identity.API.IoC;
using RoadFlow.Identity.Core;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.SetupApplicationConfigurations();
var (
    jwtConfiguration, 
    googleAuthConfiguration,
    mongoDbConfiguration, 
    allowedClientOrigins) = SharedConfigurationBuilder.BindAndValidate(builder.Configuration);

builder.Services.AddSingleton(jwtConfiguration);
builder.Services.AddSingleton(googleAuthConfiguration);
builder.Services.AddSingleton(mongoDbConfiguration);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("../logs/RoadFlowIdentityAPI.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddSingleton(Log.Logger);

builder.Services.AddCors(options =>
{
    options.AddPolicy("RoadFlowCorsPolicy", configurePolicy =>
    {
        configurePolicy
            .WithOrigins(allowedClientOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/google/sign-in";
    })
    .AddAuthenticationByJwtBearer(jwtConfiguration)
    .AddAuthenticationByGoogleOAuth(googleAuthConfiguration);

builder.Services.AddSingleton<IMongoContext>(new MongoContext(mongoDbConfiguration));

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

app.UseCors("RoadFlowCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.RunMigrations();

app.Run();