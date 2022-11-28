namespace RoadFlow.Common.Exceptions;

public class ClientException : Exception
{
    public string ErrorCode { get; }
    
    public ClientException(string errorCode, string? message = null) : base(message)
    {
        ErrorCode = errorCode;
    }
}