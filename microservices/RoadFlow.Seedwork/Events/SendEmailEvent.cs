namespace RoadFlow.Seedwork.Events;

public record SendEmailEvent(
    string ToEmail,
    string Subject,
    string FromEmail = "support@road-flow.com",
    string PlainTextContent = null,
    string HtmlContent = null,
    string FromName = "RoadFlow",
    string ToName = null);