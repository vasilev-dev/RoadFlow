using AspNetCore.Identity.Mongo;
using FluentValidation;
using RoadFlow.Auth.API;
using RoadFlow.Auth.Common.Configurations;
using RoadFlow.Auth.Domain;
using RoadFlow.Auth.IdentityServer;
using RoadFlow.Common.Extensions;
using RoadFlow.Common.Middlewares;
using RoadFlow.Seedwork.ApplicationUser;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityMongoDbProvider<ApplicationUser, ApplicationRole, string>(identity => 
{
    identity.User.RequireUniqueEmail = true;
    identity.Password.RequireDigit = true;
    identity.Password.RequireLowercase = true;
    identity.Password.RequireUppercase = true;
    identity.Password.RequireNonAlphanumeric = true;
}, mongo =>
{
    mongo.ConnectionString = builder.Configuration.GetConnectionString("MongoDB");
});

var sharedConfiguration = builder.AddSharedConfiguration();

var identityServerConfiguration = new IdentityServerConfiguration();
builder.Configuration.GetSection("IdentityServerSettings").Bind(identityServerConfiguration);
builder.Services.AddSingleton(identityServerConfiguration);

builder.Services.AddRoadFlowLogger(RoadFlowAuthAPI.Assembly);
builder.Services.AddRoadFlowAuthentication(sharedConfiguration.IdentitySettings);
builder.Services.AddValidatorsFromAssembly(RoadFlowAuthDomain.Assembly);
builder.Services.AddRoadFlowMediatR(RoadFlowAuthDomain.Assembly);

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