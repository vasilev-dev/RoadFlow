using System.Net;

namespace RoadFlow.Common.Exceptions;

public class ClientException : ExceptionWithErrorCode
{
    public ClientException(string errorCode, string message) : base(errorCode, message, HttpStatusCode.BadRequest)
    {
    }
}