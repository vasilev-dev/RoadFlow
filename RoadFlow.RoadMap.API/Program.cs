using RoadFlow.Common.Authentication;
using RoadFlow.Common.Configurations;
using RoadFlow.Common.Errors;
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

// todo run migration here
// app.RunMigrations();

app.Run();