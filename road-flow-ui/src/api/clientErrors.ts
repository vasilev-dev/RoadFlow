export const ClientError = {
    UserWithEmailAlreadyExistsError: 'UserWithEmailAlreadyExistsError',
    UserDoesNotExist: 'UserDoesNotExist',
    WrongPassword: 'WrongPassword',
};

export const ClientErrorMessage = new Map<string, string>([
    [ClientError.UserWithEmailAlreadyExistsError, 'User with passed email already exists'],
    [ClientError.UserDoesNotExist, "User doesn't exists"],
    [ClientError.WrongPassword, 'Wrong password'],
]);
