type TokenResponse = {
    accessToken: string;
    expirationAccessTokenTime: Date;
    refreshToken: string;
    expirationRefreshTokenTime: Date;
};

export default TokenResponse;
