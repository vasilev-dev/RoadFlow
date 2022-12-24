using MassTransit;
using RoadFlow.Common.Extensions;
using RoadFlow.Notifications.API;
using RoadFlow.Notifications.Common.Configurations;
using RoadFlow.Notifications.Domain.SendEmail;

var builder = WebApplication.CreateBuilder(args);

var sharedConfiguration = builder.AddSharedConfiguration();

builder.Configuration.AddUserSecrets<SendGridConfiguration>();
var sendGridConfiguration = new SendGridConfiguration();
builder.Configuration.GetSection("SendGridConfiguration").Bind(sendGridConfiguration);
builder.Services.AddSingleton(sendGridConfiguration);

builder.Services.AddRoadFlowLogger(RoadFlowNotificationsAPI.Assembly);

builder.Services.AddMassTransit(x =>
{
    var rabbitMQConfiguration = sharedConfiguration.RabbitMQConfiguration;

    x.AddConsumer<SendEmailEventConsumer>();
    
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

        configuration.ReceiveEndpoint("email_notifications", 
            endpoint => endpoint.ConfigureConsumer<SendEmailEventConsumer>(context));
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();