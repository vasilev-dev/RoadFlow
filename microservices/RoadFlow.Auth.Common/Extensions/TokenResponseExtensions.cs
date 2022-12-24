using IdentityModel.Client;

namespace RoadFlow.Auth.Common.Extensions;

public static class TokenResponseExtensions
{
    private const string InvalidGrantError = "invalid_grant";
    private const string InvalidUsernameOrPasswordErrorDescription = "invalid_username_or_password";
    
    public static bool WrongEmailOrPassword(this TokenResponse tokenResponse) => 
        tokenResponse.IsError &&
        tokenResponse is {Error: InvalidGrantError, ErrorDescription: InvalidUsernameOrPasswordErrorDescription};

    public static bool UserNotActivated(this TokenResponse tokenResponse) =>
        tokenResponse.IsError && tokenResponse is {Error: InvalidGrantError, ErrorDescription: null};
}