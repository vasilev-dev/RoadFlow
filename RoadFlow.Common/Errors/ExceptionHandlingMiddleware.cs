using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace RoadFlow.Common.Errors;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception.Message);
        
        context.Response.ContentType = "application/json";
        switch (exception)
        {
            case ClientException clientException:
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    errorCode = clientException.ErrorCode.ToString(),
                    message = clientException.Message
                }));
                break;
            case ValidationException validationException:
                context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    errorCode = "ValidationError",
                    validationErrors = validationException.Errors.Select(e => new
                    {
                        errorCode = e.ErrorCode,
                        errorMessage = e.ErrorMessage,
                        propertyName = e.PropertyName
                    })
                }));
                break;
            default:
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    errorCode = "InternalError"
                }));
                break;
        }
    }
}