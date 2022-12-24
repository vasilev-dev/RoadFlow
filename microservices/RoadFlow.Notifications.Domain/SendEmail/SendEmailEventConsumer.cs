using MassTransit;
using RoadFlow.Common.Exceptions;
using RoadFlow.Common.Extensions;
using RoadFlow.Notifications.Common.Configurations;
using RoadFlow.Seedwork.Events;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;

namespace RoadFlow.Notifications.Domain.SendEmail;

public class SendEmailEventConsumer : IConsumer<SendEmailEvent>
{
    private readonly ILogger _logger;
    private readonly SendGridClient _client;

    public SendEmailEventConsumer( 
        ILogger logger,
        SendGridConfiguration sendGridConfiguration)
    {
        _logger = ArgumentNullValidator.ThrowIfNullOrReturn(logger);
        ArgumentException.ThrowIfNullOrEmpty(sendGridConfiguration.APIKey);
        _client = new SendGridClient(sendGridConfiguration.APIKey);
    }

    public async Task Consume(ConsumeContext<SendEmailEvent> context)
    {
        var message = context.Message;
        
        var from = new EmailAddress(message.FromEmail, message.FromName);
        var to = new EmailAddress(message.ToEmail, message.ToName);
        var msg = MailHelper.CreateSingleEmail(from, to, message.Subject, 
            message.PlainTextContent, message.HtmlContent);
        
        var response = await _client.SendEmailAsync(msg);

        if (response is null)
        {
            _logger.Error("Cannot send email notification: response is null");
            throw new ServerException("Cannot send email notification");
        }

        if (!response.IsSuccessStatusCode)
        {
            var bodyContent = await response.Body.ReadAsStringAsync();
            _logger.Error("Cannot send email notifications: StatusCode = {0}, Body = {@Body}", 
                response.StatusCode, bodyContent);
            throw new ServerException("Cannot send email notification");
        }
    }
}