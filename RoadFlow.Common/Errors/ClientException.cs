namespace RoadFlow.Common.Errors;

public class ClientException : Exception
{
    public ClientErrorCode ErrorCode { get; }
    public override string Message { get; }

    public ClientException(ClientErrorCode clientErrorCode, string message = null)
    {
        ErrorCode = clientErrorCode;
        Message = message;
    }
}