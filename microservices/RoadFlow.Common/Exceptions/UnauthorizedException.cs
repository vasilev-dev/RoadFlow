using System.Net;

namespace RoadFlow.Common.Exceptions;

public class UnauthorizedException: ExceptionWithErrorCode
{
    public UnauthorizedException(string errorCode, string message) : base(errorCode, message, HttpStatusCode.Unauthorized)
    {
    }
}