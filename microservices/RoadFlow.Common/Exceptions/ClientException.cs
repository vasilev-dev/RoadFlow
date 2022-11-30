namespace RoadFlow.Common.Exceptions;

public class ClientException : Exception
{
    public string ErrorCode { get; }
    
    public ClientException(string errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }
}