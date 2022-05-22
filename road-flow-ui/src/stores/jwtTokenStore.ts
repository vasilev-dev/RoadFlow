import Cookies, {CookieSetOptions} from 'universal-cookie';

export default class JwtTokenStore {
    private _cookies = new Cookies();

    private static _privateCookieOptions: CookieSetOptions = {
        path: '/',
        httpOnly: false, // todo set true if yandex cloud will work on https (load from configuration)
        secure: false, // todo set true if client app + microservice could work with it (load from configuration)
        sameSite: 'strict',
    };

    public setAccessToken(accessToken: string, expirationAccessTokenTime: Date): void {
        this._cookies.set('access_token', accessToken, {
            ...JwtTokenStore._privateCookieOptions,
            expires: new Date(expirationAccessTokenTime),
        });
        this.setExpirationAccessTokenTime(expirationAccessTokenTime);
    }

    private setExpirationAccessTokenTime(expirationAccessTokenTime: Date): void {
        this._cookies.set(
            'expiration_access_token_time',
            expirationAccessTokenTime,
            JwtTokenStore._privateCookieOptions
        );
    }

    public setRefreshToken(refreshToken: string, expirationRefreshTokenTime: Date): void {
        const test = new Date();
        test.setFullYear(2024);

        this._cookies.set('refresh_token', refreshToken, {
            ...JwtTokenStore._privateCookieOptions,
            expires: new Date(expirationRefreshTokenTime),
        });
        this.setExpirationRefreshTokenTime(expirationRefreshTokenTime);
    }

    private setExpirationRefreshTokenTime(expirationRefreshTokenTime: Date): void {
        this._cookies.set(
            'expiration_refresh_token_time',
            expirationRefreshTokenTime,
            JwtTokenStore._privateCookieOptions
        );
    }

    get accessToken(): string | undefined {
        return this._cookies.get('access_token');
    }

    // todo save user info
}
