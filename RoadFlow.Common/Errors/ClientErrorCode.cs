namespace RoadFlow.Common.Errors;

public enum ClientErrorCode
{
    UserWithEmailAlreadyExistsError,
    UserWithUsernameAlreadyExistsError,
    UserDoesNotExist,
    WrongPassword
}