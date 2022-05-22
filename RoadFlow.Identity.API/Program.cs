using FluentValidation;
using MediatR;
using RoadFlow.Common.Authentication;
using RoadFlow.Common.Configurations;
using RoadFlow.Common.Configurations.MongoDb;
using RoadFlow.Common.Errors;
using RoadFlow.Common.Mediator;
using RoadFlow.Data;
using RoadFlow.Identity.API.IoC;
using RoadFlow.Identity.Core;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddRoadFlowApplicationConfigurations();

builder.Services.AddApplicationAuthentication();

// todo вынести в отдельный класс в common project (передавать Assembly project name)
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("../logs/RoadFlowIdentityAPI.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Services.AddSingleton(Log.Logger);


// todo вынести в отдельный класс в common project
builder.Services.AddSingleton<IMongoContext>(services => new MongoContext(services.GetService<MongoDbConfiguration>()!));
// builder.Services.AddSingleton<IMongoContext>(new MongoContext(mongoDbConfiguration));

builder.Services.RegisterAppRepositories();
builder.Services.RegisterAppServices();

builder.Services.AddValidatorsFromAssembly(IndentityCoreProjectAssembly.Assembly);

// todo вынести в отдельный класс в common project
builder.Services.AddMediatR(IndentityCoreProjectAssembly.Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PipelineBehavior<,>));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // todo что это?
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

app.RunMigrations();

app.Run();