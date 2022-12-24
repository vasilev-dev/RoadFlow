using AspNetCore.Identity.Mongo;
using FluentValidation;
using MassTransit;
using MongoDB.Driver;
using RoadFlow.Auth.API;
using RoadFlow.Auth.Common.Configurations;
using RoadFlow.Auth.DAL;
using RoadFlow.Auth.Domain;
using RoadFlow.Auth.Domain.ConfirmAccount;
using RoadFlow.Auth.IdentityServer;
using RoadFlow.Common.Extensions;
using RoadFlow.Common.Middlewares;
using RoadFlow.Seedwork.ApplicationUser;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mongoDbConfiguration = builder.Configuration.GetSection("MongoDbConfiguration").Get<MongoDbConfiguration>();

builder.Services.AddIdentityMongoDbProvider<ApplicationUser, ApplicationRole, string>(identity => 
{
    identity.User.RequireUniqueEmail = true;
    identity.Password.RequireDigit = true;
    identity.Password.RequireLowercase = true;
    identity.Password.RequireUppercase = true;
    identity.Password.RequireNonAlphanumeric = true;
}, mongo =>
{
    mongo.ConnectionString = mongoDbConfiguration.FullConnectionString;
});

var mongoClient = new MongoClient(mongoDbConfiguration.ConnectionString);
builder.Services.AddSingleton<IMongoClient>(mongoClient);
builder.Services.AddSingleton(mongoClient.GetDatabase(mongoDbConfiguration.DatabaseName));

builder.Services.AddScoped<IAccountConfirmationRepository, AccountConfirmationRepository>();

var sharedConfiguration = builder.AddSharedConfiguration();
var identityServerConfiguration = new IdentityServerConfiguration();
builder.Configuration.GetSection("IdentityServerConfiguration").Bind(identityServerConfiguration);
builder.Services.AddSingleton(identityServerConfiguration);

builder.Services.AddRoadFlowLogger(RoadFlowAuthAPI.Assembly);
builder.Services.AddRoadFlowAuthentication(sharedConfiguration.IdentityConfiguration);
builder.Services.AddValidatorsFromAssembly(RoadFlowAuthDomain.Assembly);
builder.Services.AddRoadFlowMediatR(RoadFlowAuthDomain.Assembly);

builder.Services.AddMassTransit(x =>
{
    var rabbitMQConfiguration = sharedConfiguration.RabbitMQConfiguration;
    
    x.UsingRabbitMq((context, configuration) =>
    {
        configuration.Host(
            rabbitMQConfiguration.Host,
            rabbitMQConfiguration.Port,
            rabbitMQConfiguration.VHost,
            x =>
            {
                x.Username(rabbitMQConfiguration.Username);
                x.Password(rabbitMQConfiguration.Password);
            });
    });
});

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