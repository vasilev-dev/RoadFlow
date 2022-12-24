using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using RoadFlow.Common.Exceptions;
using Serilog;

namespace RoadFlow.Common.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger logger)
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
        context.Response.ContentType = "application/json";
        switch (exception)
        {
            case ExceptionWithErrorCode exceptionWithErrorCode:
                context.Response.StatusCode = (int)exceptionWithErrorCode.StatusCode;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    errorCode = exceptionWithErrorCode.ErrorCode,
                    errorMessage = exceptionWithErrorCode.Message
                }));
                _logger.Warning(exceptionWithErrorCode, exceptionWithErrorCode.Message);
                break;
            
            case ValidationException validationException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    errorCode = ErrorCode.ValidationError,
                    validationErrors = validationException.Errors.Select(e => new
                    {
                        errorCode = e.ErrorCode,
                        errorMessage = e.ErrorMessage,
                        propertyName = e.PropertyName
                    })
                }));
                break;
            
            case { } internalException:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    errorCode = "InternalError",
                    errorMessage = "Something went wrong"
                }));
                _logger.Error(internalException, "Unhandled exception: {message}", 
                    internalException.Message);
                break;
        }
    }
}