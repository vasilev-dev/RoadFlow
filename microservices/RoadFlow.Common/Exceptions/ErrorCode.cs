namespace RoadFlow.Common.Exceptions;

public static class ErrorCode
{
    // 400 status error
    public const string ValidationError = "ValidationError";
    public const string UsernameAlreadyIsUsed = "UsernameAlreadyIsUsed";
    public const string EmailAlreadyIsUsed = "EmailAlreadyIsUsed";
    public const string UserNotFound = "UserNotFound";
    public const string EmailAlreadyConfirmed = "EmailAlreadyConfirmed";
    public const string ConfirmationCodeIsExpired = "ConfirmationCodeIsExpired";
    public const string WrongConfirmationCode = "WrongConfirmationCode";
    
    // 401 status error
    public const string WrongUsernameOrPassword = "WrongUsernameOrPassword";
    public const string UserNotActivated = "UserNotActivated";
}