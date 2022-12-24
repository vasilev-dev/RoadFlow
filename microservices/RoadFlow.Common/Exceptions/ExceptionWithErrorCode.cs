using System.Net;

namespace RoadFlow.Common.Exceptions;

public abstract class ExceptionWithErrorCode : Exception
{
    public string ErrorCode { get; }
    public HttpStatusCode StatusCode { get; }
    
    public ExceptionWithErrorCode(string errorCode, string message, HttpStatusCode statusCode) : base(message)
    {
        ErrorCode = errorCode;
        StatusCode = statusCode;
    }
}